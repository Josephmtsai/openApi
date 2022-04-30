using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using provider.Model;
using System;
using System.Linq;
using System.Reflection;


namespace provider.InfraStructure.Json
{
    public class RequestJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            foreach (PropertyInfo prop in value.GetType().GetProperties())
            {
                object[] attrs = prop.GetCustomAttributes(true);
                if (attrs.Any(x => x is SensitiveDataAttribute))
                    prop.SetValue(value, "[**mask**]");
            }

            var t = JToken.FromObject(value);
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
