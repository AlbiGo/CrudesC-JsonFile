using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Utilities
{
    public static class Utility
    {
        public static async Task saveAsync(JArray _JArray, string jsonFilePath)
        {

            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(_JArray, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(jsonFilePath, newJsonResult);
        }
    }
}
