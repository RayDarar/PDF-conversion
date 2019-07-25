using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PDF_conversion.src.logic
{
    public static class Startup
    {
        public static void Load()
        {
            CheckFile();
            LoadToData();
        }

        private static void LoadToData()
        {
            using (FileStream stream = new FileStream("data.json", FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
                DataModule.data = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
        }

        private static void CheckFile()
        {
            if (!File.Exists("data.json"))
            {
                string data = JsonConvert.SerializeObject(DataModule.data);
                using (FileStream stream = new FileStream("data.json", FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                    writer.Write(data);
            }
        }
    }
}
