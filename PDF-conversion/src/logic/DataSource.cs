using System;
using System.Collections.Generic;
using System.IO;

namespace PDF_conversion.src.logic
{
    public enum DataSourceType { single, multiple }

    public class DataSource
    {
        private DataSourceType type = DataSourceType.single;
        private List<FileInfo> files;

        public DataSource(string[] data, DataSourceType type)
        {
            this.type = type;
            files = new List<FileInfo>();
            foreach (var path in data)
                files.Add(new FileInfo(path));
        }

        public List<FileInfo> GetFiles() => throw new NotImplementedException();
    }
}
