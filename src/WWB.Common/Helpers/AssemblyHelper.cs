using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace WWB.Common.Helpers
{
    public class AssemblyHelper
    {
        public static AssemblyHelper Instance = new AssemblyHelper();

        private readonly Lazy<IReadOnlyList<Assembly>> _assemblies;

        public IReadOnlyList<Assembly> Assemblies => _assemblies.Value;

        public AssemblyHelper()
        {
            _assemblies = new Lazy<IReadOnlyList<Assembly>>(FindAllAssemblies, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private IReadOnlyList<Type> FindAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in Assemblies)
            {
                try
                {
                    var typesInThisAssembly = assembly.GetTypes();

                    if (!typesInThisAssembly.Any())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    //TODO: Trigger a global event?
                    Console.Write(ex);
                }
            }

            return allTypes;
        }

        public IReadOnlyList<Assembly> FindAllAssemblies()
        {
            string[] filters =
            {
            "mscorlib",
            "netstandard",
            "dotnet",
            "api-ms-win-core",
            "runtime.",
            "System",
            "Microsoft",
            "Window",
        };
            var list = new List<Assembly>();
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && !filters.Any(x => lib.Name.StartsWith(x)));
            foreach (var lib in libs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }

            return list.Distinct().ToList();
        }
    }
}