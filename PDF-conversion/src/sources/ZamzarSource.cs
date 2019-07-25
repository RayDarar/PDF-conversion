using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_conversion.src.sources
{
    public class ZamzarSource : IConversionSource
    {
        private string apiKey;

        public string FromPdfToTxt(FileInfo file)
        {
            throw new NotImplementedException();
        }

        public string GetName() => "Zamzar";

        public ZamzarSource() => apiKey = DataModule.data["zamzar.apiKey"];
    }
}
