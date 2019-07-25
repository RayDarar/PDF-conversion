using System.Collections.Generic;
using System.IO;

namespace PDF_conversion.src.logic
{
    public static class DataModule
    {
        public static Dictionary<string, string> data = new Dictionary<string, string>();

        public static void Save()
        {
            using (FileStream stream = new FileStream("data.json", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }
    }
}
