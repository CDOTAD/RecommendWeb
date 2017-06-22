using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataProcessLayer
{
    public class JsonDataProcesser
    {
        public static string ObjectToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
