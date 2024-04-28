using Newtonsoft.Json;

namespace WWB.Common.Json
{
    /// <summary>
    /// 时间格式化
    /// </summary>
    public class TimeSpanConverter : JsonConverter
    {
        //
        // 摘要:
        //     是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
        public override bool CanRead => false;

        //
        // 摘要:
        //     是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化 类型等于long时才转换
        //
        //
        // 参数:
        //   objectType:
        public override bool CanConvert(Type objectType)
        {
            if (!(objectType == typeof(TimeSpan)))
            {
                return objectType == typeof(TimeSpan?);
            }

            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false.The type will skip the converter.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            TimeSpan ts = (TimeSpan)value;

            writer.WriteValue($"{ts.Hours.ToString("#00")}:{ts.Minutes.ToString("#00")}");
        }
    }
}