using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TW.Vsix
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("T4Template")]
    internal class T4ClassifierProvider : IClassifierProvider
    {
        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

        [Import]
        private IContentTypeRegistryService contentTypeRegistryService;

        public IClassifier GetClassifier(ITextBuffer textBuffer)
        {
            return 
                textBuffer
                .Properties
                .GetOrCreateSingletonProperty(
                    () => new T4Classifier(
                        this.classificationRegistry,
                        this.contentTypeRegistryService));
        }
    }

    internal static class T4FileAndContentTypeDefinitions
    {
        [Export]
        [Name("T4Template")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition T4TemplateContentTypeDefinition;

        [Export]
        [FileExtension(".tt")]
        [ContentType("T4Template")]
        internal static FileExtensionToContentTypeDefinition T4TemplateExtensionDefinition;
    }
}
