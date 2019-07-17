using System;
using System.Collections.Generic;
using System.IO;

namespace PDF_conversion.src.logic
{
    public class DataSource
    {
        private List<FileInfo> files;

        public DataSource(string[] data)
        {
            files = new List<FileInfo>();
            foreach (var path in data)
                files.Add(new FileInfo(path));
        }

        public List<FileInfo> GetFiles() => throw new NotImplementedException();

        public string GetName() => files.Count != 0 ? files[0].Name : "Таблица";
    }
}
