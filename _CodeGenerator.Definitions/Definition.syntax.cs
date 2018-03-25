using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new DirectiveSyntaxList(nameof(DirectiveSyntaxList)),
                new StaticTextSyntax(nameof(StaticTextSyntax)),
                new ControlBlock(),
                new DirectiveSyntax(),
                new DirectiveContentsSyntax(nameof(DirectiveContentsSyntax)),
                new DirectiveParameterSyntaxList(nameof(DirectiveParameterSyntaxList)),
                new DirectiveParameterSyntax(nameof(DirectiveParameterSyntax)),
                new DirectiveParameterIdentifierSyntax(nameof(DirectiveParameterIdentifierSyntax)),
                new DirectiveNameSyntax(nameof(DirectiveNameSyntax)) }
            );

        public static (SyntaxDefinitionClassInfoProvider classInfo, ISyntaxPart syntaxPart)[] SyntaxParts =>
            Syntax
            .syntaxParts
            .Select(x => (new SyntaxDefinitionClassInfoProvider(x), x))
            .ToArray();
    }
}
