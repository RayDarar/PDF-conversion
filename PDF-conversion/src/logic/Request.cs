using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using System;

namespace PDF_conversion.src.Logic
{
    public class Request
    {
        private DataSource dataSource;
        private IDataParser parser;

        public Request(DataSource dataSource, IDataParser parser)
        {
            this.dataSource = dataSource;
            this.parser = parser;
        }

        /**
         * Executing the request
         * 
         * Algorithm:
         * In the view we choosed 
         */
        public void Execute()
        {
            Logger.Log("Started request at: " + DateTime.Now.ToShortDateString());

            try { parser.Parse(dataSource); }
            catch (Exception ex)
            {
                Logger.Log("Request failed");
                Logger.Log("Exception: " + ex.Message);
                Logger.Log("Stack trace: " + ex.StackTrace);
            }


            Logger.Log("Finished request at: " + DateTime.Now.ToShortDateString());
        }
    }
}
