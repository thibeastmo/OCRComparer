using System.Collections.Generic;

namespace OCRComparer.Execution.OCRSpace
{
    internal class Result
    {
        public List<ParsedResult> ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }
}
