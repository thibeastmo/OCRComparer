using System.Collections.Generic;

namespace OCRComparer.Execution.Google
{
    internal class Property
    {
        public List<DetectedLanguage> detectedLanguages { get; set; }
        public DetectedBreak detectedBreak { get; set; }
    }
}
