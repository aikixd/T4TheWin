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
                new TemplateBodySyntaxList(),
                new DirectiveSyntaxList(nameof(DirectiveSyntaxList)),
                new StaticTextSyntax(nameof(StaticTextSyntax)),
                new StaticTextSyntaxList(),
                new ControlBlock(),
                new ControlBlockStream(nameof(ControlBlockStream)),
                new ControlBlockStreamList(),
                new ClassFeatureBlock(nameof(ClassFeatureBlock)),
                new DirectiveSyntax(),
                new DirectiveContentsSyntax(nameof(DirectiveContentsSyntax)),
                new DirectiveParameterSyntaxList(nameof(DirectiveParameterSyntaxList)),
                new DirectiveParameterSyntax(nameof(DirectiveParameterSyntax)),
                new DirectiveParameterIdentifierSyntax(nameof(DirectiveParameterIdentifierSyntax)),
                new DirectiveNameSyntax(nameof(DirectiveNameSyntax)),
                new TemplateTextToken(nameof(TemplateTextToken)),
                new ControlBlockTagOpenToken(nameof(ControlBlockTagOpenToken)),
                new DirectiveBlockTagOpenToken(nameof(DirectiveBlockTagOpenToken)),
                new ClassFeatureBlockOpenTagToken(nameof(ClassFeatureBlockOpenTagToken)),
                new BlockTagCloseToken(nameof(BlockTagCloseToken)),
                new SourceCodeToken(nameof(SourceCodeToken)),
                new EqualsToken(nameof(EqualsToken)),
                new IdentifierToken(nameof(IdentifierToken)),
                new StringLiteral(nameof(StringLiteral)),
                new StringLiteralText(nameof(StringLiteralText)),
                new StringLiteralTextList(nameof(StringLiteralTextList)),
                new StringLiteralTextToken(nameof(StringLiteralTextToken)),
                new StringDelimiterToken(nameof(StringDelimiterToken))
            });

        public static (SyntaxDefinitionClassInfoProvider classInfo, ISyntaxPart syntaxPart)[] SyntaxParts =>
            Syntax
            .syntaxParts
            .Select(x => (new SyntaxDefinitionClassInfoProvider(x), x))
            .ToArray();
    }
}
