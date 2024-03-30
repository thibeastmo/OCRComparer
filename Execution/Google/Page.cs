using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class Page
    {
        public Property property { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<Block> blocks { get; set; }
    }
}
