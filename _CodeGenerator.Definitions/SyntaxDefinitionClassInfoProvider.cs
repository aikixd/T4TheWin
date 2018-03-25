using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public class SyntaxDefinitionClassInfoProvider : IClassInfoProvider
    {
        private ISyntaxPart part;

        public string Name { get; }

        public string Namespace => DefinitionGeneral.SyntaxNamespace;

        public Accessabilty Accessability => Accessabilty.Inherited;

        public bool IsStatic => false;

        public bool IsSealed => false;

        public IEnumerable<ClassMember> Members => Enumerable.Empty<ClassMember>();

        public IEnumerable<Domain.Attribute> Attributes => Enumerable.Empty<Domain.Attribute>();

        public SyntaxDefinitionClassInfoProvider(ISyntaxPart part)
        {
            this.part = part;
            this.Name = part.GetType().Name;
        }
    }
}
