using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class Block
    {
        public BoundingBox boundingBox { get; set; }
        public List<Paragraph> paragraphs { get; set; }
        public string blockType { get; set; }
    }
}
