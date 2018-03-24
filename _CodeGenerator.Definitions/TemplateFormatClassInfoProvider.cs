using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public class TemplateFormatClassInfoProvider : IClassInfoProvider
    {
        public Classification Classification { get; }

        internal TemplateFormatClassInfoProvider(Classification classification)
        {
            this.Classification = classification;
        }

        public string Name => $"{this.Classification.Name.Replace(".", "")}Format";

        public string Namespace => $"{DefinitionGeneral.ClassifierNamespace}";

        public Accessabilty Accessability => Accessabilty.Internal;

        public bool IsStatic => false;

        public bool IsSealed => true;

        public IEnumerable<ClassMember> Members => Enumerable.Empty<ClassMember>();

        public IEnumerable<Domain.Attribute> Attributes => Enumerable.Empty<Domain.Attribute>();
    }
}
