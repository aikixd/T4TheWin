using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class ContentInterfaceTemplate
    {
        public ContentInterfaceTemplate(IClassInfoProvider classInfo, ISyntaxPart syntaxPart)
        {
            this.ClassInfo = classInfo;
            this.SyntaxPart = syntaxPart;
        }

        public IClassInfoProvider ClassInfo { get; }
        public ISyntaxPart SyntaxPart { get; }
    }
}
