namespace OCRComparer.Execution.Google
{
    internal class TextAnnotation
    {
        public string locale { get; set; }
        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
    }
}
