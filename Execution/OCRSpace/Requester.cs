using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace OCRComparer.Execution.OCRSpace
{
    internal class Requester
    {
        public static async Task<string> PostRequest(string imagePath)
        {
            string url = "https://api.ocr.space/parse/image";
            var imageBytes = File.ReadAllBytes(imagePath);

            using (var client = new HttpClient())
            {
                // Set the API key header
                client.DefaultRequestHeaders.Add("apikey", ""); //fill in your key

                // Create the form-data content
                var formContent = new MultipartFormDataContent();

                // Add the image as a content stream
                var imageContent = new StreamContent(new MemoryStream(imageBytes));
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                var fileName = Path.GetFileName(imagePath);
                formContent.Add(imageContent, fileName.Replace(".png", string.Empty), fileName);

                // Add the parameters as string content
                formContent.Add(new StringContent("dut"), "language");
                formContent.Add(new StringContent("true"), "isOverlayRequired");
                formContent.Add(new StringContent("PNG"), "filetype");
                formContent.Add(new StringContent("false"), "detectOrientation");
                formContent.Add(new StringContent("false"), "isCreateSearchablePdf");
                formContent.Add(new StringContent("true"), "scale");
                formContent.Add(new StringContent("2"), "OCREngine");

                // Send the POST request
                HttpResponseMessage response = null;

                bool ok = false;
                for (var i = 0; !ok && i < 5; i++)
                {
                    response = client.PostAsync(url, formContent).Result;
                    ok = response.IsSuccessStatusCode;
                }
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return null;
        }
    }
}
