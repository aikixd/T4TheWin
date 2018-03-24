using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public partial class Definition
    {
        public static (CustomClassInfoProvider classInfo, Whitespace whitespace) Whitespace =>
            (new CustomClassInfoProvider(
                "Scanner",
                DefinitionGeneral.SyntaxNamespace), 
            new Whitespace());
    }
}
