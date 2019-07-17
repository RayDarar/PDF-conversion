using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using System;

namespace PDF_conversion.src.Logic
{
    public class Request
    {
        private DataSource dataSource;
        private DataTemplateBase parser;

        public Request(DataSource dataSource, DataTemplateBase parser)
        {
            this.dataSource = dataSource;
            this.parser = parser;
        }

        /**
         * Executing the request
         * 
         * Algorithm:
         * In the view we choosed data source (ex PDF file), multiple files or single file
         * Also user choosed DataFormat or file template
         * Depending on such template, we'll execute different parsers
         * Parser will give us the result in chosen DataFormat
         * This result will go to GenerateModel class, which will generate excel depending on DataFormat
         * 
         */
        public void Execute()
        {
            Logger.Log("Started request at: " + DateTime.Now.ToString());

            try { parser.Parse(dataSource); }
            catch (Exception ex)
            {
                Logger.Log("Request failed");
                Logger.Log("Exception: " + ex.Message);
                Logger.Log("Stack trace: " + ex.StackTrace);
            }

            Logger.Log("Finished request at: " + DateTime.Now.ToString());
        }
    }
}
