using System.Collections.Generic;

namespace OCRComparer.Execution.OCRSpace
{
    internal class TextOverlay
    {
        public List<object> Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }
}
