using System.Collections.Generic;

namespace PDF_conversion.src.interfaces
{
    public abstract class DataFormatBase
    {
        private Dictionary<string, object> data;
        private string[] keys;

        public DataFormatBase(string[] keys)
        {
            this.keys = keys;
            foreach (var key in keys)
                data.Add(key, null);
        }

        public object Get(string key) => data.ContainsKey(key) ? data[key] : throw new KeyNotFoundException();
        public void Set(string key, object value)
        {
            if (data.ContainsKey(key))
                data[key] = value;
            else
                throw new KeyNotFoundException();
        }

        public string[] GetKeys() => keys;
    }
}
