using PDF_conversion.src.interfaces;
using System;
using System.IO;

namespace PDF_conversion.src.logic
{
    public class Request : IRequest
    {
        private IConversionSource source;
        private ITemplate template;
        private FileInfo[] files;

        public bool Convert()
        {
            try
            {
                Logger.Log($"Request at: {DateTime.Now.ToString()}");
                for (int i = 0; i < files.Length; i++)
                {
                    Logger.Log($"File # {i + 1}");

                    var file = files[i];

                    Logger.Log("From pdf to txt started");
                    string rawText = source.FromPdfToTxt(file); // Converting from pdf to txt
                    Logger.Log("From pdf to txt ended");

                    Logger.Log("To excel started");
                    template.ToExcel(rawText);
                    Logger.Log("To excel ended\n");
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception message: {ex}");
                return false;
            }
        }

        public void SetConversionSource(IConversionSource source) => this.source = source;
        public void SetFiles(FileInfo[] files) => this.files = files;
        public void SetTemplate(ITemplate template) => this.template = template;
    }
}
