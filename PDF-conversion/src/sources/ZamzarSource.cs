using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using System.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PDF_conversion.src.sources
{
    public class ZamzarSource : IConversionSource
    {
        private string apiKey;

        public string FromPdfToTxt(FileInfo file)
        {
            // First, upload the job
            const string url = "https://sandbox.zamzar.com/v1/jobs/";

            var job = Upload(apiKey, url, file.FullName, "txt").Result;

        Query:
            // Check if it's done
            var query = Query(apiKey, url + job["id"].ToString()).Result;

            var status = query["status"].ToString().Replace("\"", "");

            if (status == "successful")
            {
                // Download the file

                string contentUrl = "https://sandbox.zamzar.com/v1/files/" + query["target_files"][0]["id"].ToString() + "/content";

                Download(apiKey, contentUrl).Wait();

                return result;
            }
            else if (status == "converting")
                goto Query;
            else
                throw new Exception("Conversion failed");
        }

        private static string result = "";

        private static async Task<JsonValue> Upload(string key, string url, string sourceFile, string targetFormat)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            {
                var request = new MultipartFormDataContent();
                request.Add(new StringContent(targetFormat), "target_format");
                request.Add(new StreamContent(File.OpenRead(sourceFile)), "source_file", new FileInfo(sourceFile).Name);
                using (HttpResponseMessage response = await client.PostAsync(url, request).ConfigureAwait(false))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    return JsonValue.Parse(data);
                }
            }
        }
        private static async Task<JsonValue> Query(string key, string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                return JsonValue.Parse(data);
            }
        }
        
        private static async Task<string> Download(string key, string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false))
            using (HttpContent content = response.Content)
            using (Stream stream = await content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream))
                result = reader.ReadToEnd();
            return result;
        }

        public string GetName() => "Zamzar";

        public ZamzarSource() => apiKey = DataModule.data["zamzar.apiKey"];
    }
}
