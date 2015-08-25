namespace AgarIo.SystemExtension
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class JsonStringExtensions
    {
        public static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultJsonSerializerSettings);
        }

        public static string ToJson<T>(this T obj, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        public static T FromJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj, DefaultJsonSerializerSettings);
        }

        public static T FromJson<T>(this string obj, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(obj, jsonSerializerSettings);
        }
    }
}