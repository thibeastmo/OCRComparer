using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class Paragraph
    {
        public BoundingBox boundingBox { get; set; }
        public List<Word> words { get; set; }
    }
}
