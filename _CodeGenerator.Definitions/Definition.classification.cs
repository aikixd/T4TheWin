using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public partial class Definition
    {
        internal const string T4Template = "T4Template";
        internal const string T4TemplateControl = "T4Template.control";
        internal const string T4TemplateControlDelimiter = "T4Template.control.delimiter";
        internal const string T4TemplateControlDelimiterStart = "T4Template.control.delimiter.start";
        internal const string T4TemplateControlDelimiterEnd = "T4Template.control.delimiter.end";
        internal const string T4TemplateStatic = "T4Template.static";

        public static Classification[] Classifications =>
            new Classification[]
            {
                new Classification
                {
                    Name = Definition.T4Template,
                    BaseClassification = null,
                    UserVisible = false
                },
                new Classification
                {
                    Name = Definition.T4TemplateControl,
                    BaseClassification = Definition.T4Template,
                    UserVisible = true
                },
                new Classification
                {
                    Name = Definition.T4TemplateControlDelimiter,
                    BaseClassification = Definition.T4TemplateControl,
                    UserVisible = true,
                    Format = new ClassificationFormat
                    {
                        BackgroundColor = "Colors.Cyan"
                    }
                },
                new Classification
                {
                    Name = Definition.T4TemplateControlDelimiterStart,
                    BaseClassification = Definition.T4TemplateControlDelimiter,
                    UserVisible = false
                },
                new Classification
                {
                    Name = Definition.T4TemplateControlDelimiterEnd,
                    BaseClassification = Definition.T4TemplateControlDelimiter,
                    UserVisible = false
                },
                new Classification
                {
                    Name = Definition.T4TemplateStatic,
                    BaseClassification = Definition.T4Template,
                    UserVisible = true
                }
            };

        public static ClassificationDefinitionClassInfoProvider ClassificationDefinition =>
            new ClassificationDefinitionClassInfoProvider(Definition.Classifications);

        public static TemplateFormatClassInfoProvider[] TemplateFormats =>
            Definition
            .Classifications
            .Select(x => new TemplateFormatClassInfoProvider(x))
            .ToArray();
    }
}
