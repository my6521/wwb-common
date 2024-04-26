using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WWB.AspNetCore.Extensions;
using WWB.AspNetCore.Filters;
using WWB.AspNetCore.Middleware;
using WWB.Common.Helpers;
using WWB.Common.Json;
using WWB.Common.Websockets;
using WWB.Sample.API.Handler;

namespace WWB.Sample.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureDynamicProxy(c => { c.ThrowAspectException = false; });
            builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            builder.Services
                .AddControllers(x => { x.Filters.Add(typeof(ModelValidateActionFilter)); })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.Converters.Add(new LongToStringConverter());
                });

            var assemblies = AssemblyHelper.Instance.Assemblies.ToList();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersionSetup();
            builder.Services.AddSwaggerSetup();
            builder.Services.AddSmartSqlSetup(builder.Configuration, assemblies);
            builder.Services.AddServicesFromAllAssembly();
            builder.Services.AddMapsterSetup(assemblies.ToArray());
            builder.Services.AddMediatR(c => { c.RegisterServicesFromAssemblies(assemblies.ToArray()); });

            builder.Services.Replace(ServiceDescriptor.Scoped<IWebsocketMsgHandler, WsMsgHandler>());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerSetup();
            }
            app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseMiddleware<ApiExceptionHandlerMiddleware>();
            app.UseMiddleware<WebSocketHandlerMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}