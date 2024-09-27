using Newtonsoft.Json;

namespace Shared.Infrastructure.Extensions
{
    public static class JsonExtension
    {
        public static string SerialJson(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T DeserializeJson<T>(this string data) where T : class
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static object DeserializeJson(string json, Type targetType)
        {
            if (targetType == typeof(int))
            {
                return int.Parse(json);
            }
            else if (targetType == typeof(float))
            {
                return float.Parse(json);
            }
            else if (targetType == typeof(double))
            {
                return double.Parse(json);
            }
            else if (targetType == typeof(bool))
            {
                return bool.Parse(json);
            }
            else if (targetType == typeof(string))
            {
                return json;
            }
            else
            {
                return JsonConvert.DeserializeObject(json, targetType);
            }
        }
    }
}
