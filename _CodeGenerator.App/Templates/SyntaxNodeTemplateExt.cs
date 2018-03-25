using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    public partial class SyntaxNodeTemplate
    {
        internal class SyntaxPartComparer : IEqualityComparer<ISyntaxPart>
        {
            public bool Equals(ISyntaxPart x, ISyntaxPart y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(ISyntaxPart obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        public IClassInfoProvider ClassInfo { get; }
        public ISyntaxPart SyntaxPart { get; }

        public SyntaxNodeTemplate(IClassInfoProvider classInfo, ISyntaxPart syntaxPart)
        {
            this.ClassInfo = classInfo;
            this.SyntaxPart = syntaxPart;
        }
    }
}
