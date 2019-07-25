using PDF_conversion.src.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_conversion.src.logic
{
    public class Request : IRequest
    {
        public bool Convert()
        {
            throw new NotImplementedException();
        }

        public void Log()
        {
            throw new NotImplementedException();
        }

        public void SetConversionSource(IConversionSource source)
        {
            throw new NotImplementedException();
        }

        public void SetFiles(FileInfo[] files)
        {
            throw new NotImplementedException();
        }

        public void SetTemplate(ITemplate template)
        {
            throw new NotImplementedException();
        }
    }
}
