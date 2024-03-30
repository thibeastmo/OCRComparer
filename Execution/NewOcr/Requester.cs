using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OCRComparer.Execution.NewOcr
{
    internal class Requester
    {
        public static async Task<string> PostImageToNewOcr(string imagePath)
        {
            string apiKey = "";

            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    // Read the file
                    byte[] fileBytes = File.ReadAllBytes(imagePath);

                    // Create the content for the multipart/form-data request
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.Add("Content-Type", "image/jpeg");
                    formData.Add(fileContent, "file", Path.GetFileName(imagePath));

                    // Add the API key as a query parameter
                    var url = $"http://api.newocr.com/v1/upload?key={apiKey}";

                    // Send the POST request
                    using (var response = await client.PostAsync(url, formData))
                    {
                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<NewOcr.Upload.Root>(json);
                            return await GetOCRResultFromUploadedImage(result.data.file_id, apiKey);
                        }
                    }
                }
            }
            return null;
        }
        public static async Task<string> GetOCRResultFromUploadedImage(string fileId, string apiKey)
        {
            int pageNumber = 1;
            string language = "nld";
            int psm = 6;

            // Construct the URL for OCR recognition
            string url = $"http://api.newocr.com/v1/ocr?key={apiKey}&file_id={fileId}&page={pageNumber}&lang={language}&psm={psm}";

            using (var client = new HttpClient())
            {
                // Send the GET request
                using (var response = await client.GetAsync(url))
                {
                    // Check if the request was successful
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
