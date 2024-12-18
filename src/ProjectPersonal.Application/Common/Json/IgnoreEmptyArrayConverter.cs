using System;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectPersonal.Application.Common.Json
{
    public class IgnoreEmptyArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<object>).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value is IEnumerable<object> enumerable && !enumerable.GetEnumerator().MoveNext())
            {
                // Không ghi gì nếu mảng rỗng
                return;
            }

            // Ghi dữ liệu nếu không rỗng
            JToken.FromObject(value).WriteTo(writer);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }
    }

}
