using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TW.Vsix
{
    class T4Classifier : IClassifier
    {
        private readonly IClassificationTypeRegistryService classificationTypeRegistry;
        private readonly IContentTypeRegistryService contentTypeRegistryService;

        public T4Classifier(
            IClassificationTypeRegistryService classificationTypeRegistry,
            IContentTypeRegistryService contentTypeRegistryService)
        {
            this.classificationTypeRegistry = classificationTypeRegistry;
            this.contentTypeRegistryService = contentTypeRegistryService;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var text = span.Snapshot.GetText();

            var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationTypeRegistry.GetClassificationType("T4Template"))
            };

            for (int i = 0; i < text.Length;)
            {
                var startTagIndex = text.IndexOf("<#", i);
                var endTagIndex = text.IndexOf("#>", i);

                if (startTagIndex == -1 && endTagIndex == -1)
                    break;

                if (startTagIndex == -1)
                    startTagIndex = int.MaxValue;

                if (endTagIndex == -1)
                    endTagIndex = int.MaxValue;

                var smallerIndex =
                    startTagIndex < endTagIndex ?
                    startTagIndex :
                    endTagIndex;

                result.Add(
                    new ClassificationSpan(
                        new SnapshotSpan(
                            span.Snapshot,
                            new Span(smallerIndex, 2)),
                        this.classificationTypeRegistry.GetClassificationType("T4Template.control.delimiter")));

                i = smallerIndex + 2;
            }





            return result;
        }
    }
}
