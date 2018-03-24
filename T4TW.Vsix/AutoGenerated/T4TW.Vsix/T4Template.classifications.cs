
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace T4TW.Vsix
{
    internal static class T4TemplateClassificationDefinitionClassificationDefinitions
    {
        [Export]
        [Name("T4Template")]
        private static ClassificationTypeDefinition T4TemplateDefinition;

        [Export]
        [Name("T4Template.control")]
		[BaseDefinition("T4Template")]
        private static ClassificationTypeDefinition T4TemplatecontrolDefinition;

        [Export]
        [Name("T4Template.control.delimiter")]
		[BaseDefinition("T4Template.control")]
        private static ClassificationTypeDefinition T4TemplatecontroldelimiterDefinition;

        [Export]
        [Name("T4Template.control.delimiter.start")]
		[BaseDefinition("T4Template.control.delimiter")]
        private static ClassificationTypeDefinition T4TemplatecontroldelimiterstartDefinition;

        [Export]
        [Name("T4Template.control.delimiter.end")]
		[BaseDefinition("T4Template.control.delimiter")]
        private static ClassificationTypeDefinition T4TemplatecontroldelimiterendDefinition;

        [Export]
        [Name("T4Template.static")]
		[BaseDefinition("T4Template")]
        private static ClassificationTypeDefinition T4TemplatestaticDefinition;

    }
}
