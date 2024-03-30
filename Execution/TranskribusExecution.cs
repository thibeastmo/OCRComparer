using System.IO;

namespace OCRComparer.Execution
{
    internal class TranskribusExecution
    {
        public static string GetRawTextFromResult(string resultFullPath)
        {
            return File.Exists(resultFullPath) ? File.ReadAllText(resultFullPath) : null;
        }
    }
}
