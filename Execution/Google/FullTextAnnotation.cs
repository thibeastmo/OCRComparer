using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class FullTextAnnotation
    {
        public List<Page> pages { get; set; }
        public string text { get; set; }
    }
}
