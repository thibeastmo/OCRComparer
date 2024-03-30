using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace OCRComparer.Execution.Google
{
    internal class Response
    {
        public List<TextAnnotation> textAnnotations { get; set; }
        public FullTextAnnotation fullTextAnnotation { get; set; }
    }
}
