using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class TokenListTemplate
    {
        public IClassInfoProvider ClassInfo { get; }
        public ISyntaxPart SyntaxPart { get; }

        public TokenListTemplate(IClassInfoProvider classInfo, ISyntaxPart syntaxPart)
        {
            this.ClassInfo = classInfo;
            this.SyntaxPart = syntaxPart;
        }
    }
}
