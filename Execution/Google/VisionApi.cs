using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OCRComparer.Execution.Google
{
    internal class VisionApi
    {
        public static async Task<string> PerformOCR(string pathToImage)
        {
            string apiKey = "";

            // URL for the Cloud Vision API's text detection endpoint
            string apiUrl = "https://vision.googleapis.com/v1/images:annotate?key=" + apiKey;

            // Load image data
            byte[] imageData = System.IO.File.ReadAllBytes(pathToImage);

            // Base64 encode the image data
            string base64Image = Convert.ToBase64String(imageData);

            // Construct the JSON request
            string jsonRequest = @"
        {
          ""requests"": [
            {
              ""image"": {
                ""content"": """ + base64Image + @"""
              },
              ""features"": [
                {
                  ""type"": ""TEXT_DETECTION""
                }
              ]
            }
          ]
        }";

            using (HttpClient client = new HttpClient())
            {
                // Set the content type header
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonRequest));

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
