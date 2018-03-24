using System;

namespace _CodeGenerator.Definitions
{
    public class Classification
    {
        public string Name { get; set; }
        public string BaseClassification { get; set; }
        public bool UserVisible { get; set; }
        public ClassificationFormat Format { get; set; }
    }

    public class ClassificationFormat
    {
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}
