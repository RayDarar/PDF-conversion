using PDF_conversion.src.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_conversion.src.templates
{
    public class Application8 : ITemplate
    {
        public string GetTemplateName() => "Приложение 8";

        public void ToExcel(string rawText)
        {
            throw new NotImplementedException();
        }
    }
}
