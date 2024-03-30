using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace OCRComparer.Execution
{
    internal class Api
    {
        #region Raw text retrievers
        internal static string GetRawTextFromOCRSpace(string resultFileFullPath)
        {
            string json = File.ReadAllText(resultFileFullPath);
            OCRSpace.Result result = JsonConvert.DeserializeObject<OCRSpace.Result>(json);
            StringBuilder sb = new StringBuilder();
            if (result != null && result.ParsedResults != null)
            {
                foreach (var resultItem in result.ParsedResults)
                {
                    sb.AppendLine(resultItem.ParsedText);
                }
            }
            return sb.ToString();
        }
        internal static string GetRawTextFromGoogle(string resultFileFullPath)
        {
            string json = File.ReadAllText(resultFileFullPath);
            var result = JsonConvert.DeserializeObject<Google.Root>(json);
            if (result != null && result.responses != null)
            {
                return result.responses[0].textAnnotations[0].description;
            }
            return null;
        }
        internal static string GetRawTextFromNewOcr(string resultFileFullPath)
        {
            string json = File.ReadAllText(resultFileFullPath);
            var result = JsonConvert.DeserializeObject<NewOcr.Recognize.Root>(json);
            if (result != null && result.data != null)
            {
                return result.data.text;
            }
            return null;
        }
        #endregion
    }
}
