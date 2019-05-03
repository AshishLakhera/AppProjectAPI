using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Benchmarking.Model
{
    public static class ConfigurationSettings
    {
        public static Clients Client { get; set; }
        public static int SqlCommandTimeout { get; set; }
        public static IEnumerable<ConnectionStrings> connectionStrings { get; set; }
        public static void LoadConfigurationSettings(string json)
        {
            JObject _json = JObject.Parse(json);
            if (_json != null)
            {
                var config = _json["Config"];
                if (config != null)
                {
                    Client = (Clients)Enum.Parse(typeof(Clients),Convert.ToString(config["Client"]));
                    SqlCommandTimeout = config["SqlCommandTimeout"] == null ? 300 : Convert.ToInt32(config["SqlCommandTimeout"]);
                    connectionStrings = JsonConvert.DeserializeObject<IEnumerable<ConnectionStrings>>(Convert.ToString(config["ConnectionStrings"]));
                }
            }
        }
        public class ConnectionStrings
        {
            public ConnectionStringsCollection key { get; set; }
            public string value { get; set; }
        }
    }
}
