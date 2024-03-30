using OCRComparer.Utils;
using System.IO;
using System.Text;
using Tesseract;

namespace OCRComparer.Execution
{
    internal class TesseractExecution
    {
        private static TesseractEngine TesseractEngine;
        public static string UseTesseract(string filepath)
        {
            if (TesseractEngine == null)
            {
                InitializeTesseract();
            }
            return ReadStringFromImage(filepath);
        }
        public static void InitializeTesseract()
        {
            TesseractEngine = new TesseractEngine(LocationUtil.GetFullPath("./Assets/tessdata"), "nld_old", EngineMode.LstmOnly);
        }
        public static string ReadStringFromImage(string filepath)
        {
            return ReadStringFromImage(filepath, TesseractEngine);
        }
        private static string ReadStringFromImage(string filepath, TesseractEngine engine)
        {
            StringBuilder sb = new StringBuilder();
            using (var img = Pix.LoadFromFile(filepath))
            {
                using (var page = engine.Process(img))
                {
                    var text = page.GetText();
                    //var text = page.GetHOCRText(1);
                    sb.Append(text);
                }
            }
            return sb.ToString();
        }
        public static string GetRawTextFromResult(string resultFullPath)
        {
            if (File.Exists(resultFullPath))
            {
                return File.ReadAllText(resultFullPath);
            }
            else { return null; }
        }
    }
}
