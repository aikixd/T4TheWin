using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class ScannerTemplate
    {
        public IClassInfoProvider ClassInfo { get; }
        public Whitespace Whitespace { get; }

        public ScannerTemplate(IClassInfoProvider classInfo, Whitespace whitespace)
        {
            this.ClassInfo = classInfo;
            this.Whitespace = whitespace;
        }
    }
}
