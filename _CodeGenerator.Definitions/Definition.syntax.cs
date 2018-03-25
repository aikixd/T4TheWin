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

        public static (CustomClassInfoProvider classInfo, ISyntaxPart[] syntaxParts) Syntax =>
            (new CustomClassInfoProvider(
                "Parser",
                DefinitionGeneral.SyntaxNamespace),
            new ISyntaxPart[] {
                new TemplateSyntax(),
                new TemplateBodySyntax(),
                new DirectiveSyntaxList(),
                new StaticTextLiteral(),
                new ControlBlock(),
                new DirectiveSyntax(),
                new DirectiveContentsSyntax(),
                new DirectiveParameterSyntaxList(),
                new DirectiveParameterSyntax(),
                new DirectiveParameterIdentifierSyntax(),
                new DirectiveNameSyntax() }
            );
    }
}
