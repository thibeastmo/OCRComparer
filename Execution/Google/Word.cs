using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class Word
    {
        public BoundingBox boundingBox { get; set; }
        public List<Symbol> symbols { get; set; }
        public Property property { get; set; }
    }
}
