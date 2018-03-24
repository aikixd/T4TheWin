using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public class ClassificationDefinitionClassInfoProvider : IClassInfoProvider
    {
        public Classification[] Classifications { get; }

        internal ClassificationDefinitionClassInfoProvider(Classification[] classification)
        {
            this.Classifications = classification;
        }

        public string Name => $"{DefinitionGeneral.ClassifierName}ClassificationDefinition";

        public string Namespace => $"{DefinitionGeneral.ClassifierNamespace}";

        public Accessabilty Accessability => Accessabilty.Internal;

        public bool IsStatic => true;

        public bool IsSealed => false;

        public IEnumerable<ClassMember> Members => 
            Classifications
            .Select(x => 
                new ClassMember(
                    x.Name, 
                    new Domain.Type(
                        "ClassificationTypeDefinition", 
                        "Microsoft.VisualStudio.Text.Classification")));

        public IEnumerable<Domain.Attribute> Attributes => Enumerable.Empty<Domain.Attribute>();
    }
}
