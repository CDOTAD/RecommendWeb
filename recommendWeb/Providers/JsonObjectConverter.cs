using System;
using Newtonsoft.Json;

namespace recommendWeb.Providers
{
    public class JsonObjectConverter
    {
        public static string ObjectToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}