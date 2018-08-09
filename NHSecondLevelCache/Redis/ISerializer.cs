using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NHSecondLevelCache.Redis
{
    public interface ISerializer
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string serializedData);
    }

    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer()
        {
            _settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            _settings.Converters.Add(new StringEnumConverter());
        }

        string ISerializer.Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        T ISerializer.Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _settings);
        }
    }
}
