using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Definitions.Syntax
{
    public interface ISyntaxPart
    {
        string Name { get; }
    }

    public interface IBlock { }

    public class Whitespace : ISyntaxPart
    {
        public string Name => "Whitespace";

        public string[] Chars =>
            new string[]
            {
                " ",
                @"\t",
                @"\r",
                @"\n"
            };
    }

    public abstract class Token : ISyntaxPart
    {
        public string Name { get; }

        public string Text { get; }

        public bool Optional { get; set; }

        public Token SetOptional(bool optional)
        {
            this.Optional = optional;
            return this;
        }

        public Token(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }
    }

    [Flags]
    public enum DynamicTokenFlags
    {
        None = 0,
        CustomParse = 1 << 0
    }

    public abstract class DynamicToken : ISyntaxPart
    {
        public string Name { get; }
        public DynamicTokenFlags Flags { get; }

        public DynamicToken(string name, DynamicTokenFlags flags = DynamicTokenFlags.None)
        {
            this.Name = name;
            this.Flags = flags;
        }
    }

    public abstract class Syntax : ISyntaxPart
    {
        public SyntaxCombination[] Combinations { get; }
        public string Name { get; }

        public Syntax(string name, params SyntaxCombination[] combinations)
        {
            this.Name = name;
            this.Combinations = combinations;
        }

        public Syntax(string name, params ISyntaxPart[] syntax)
            : this(name, new SyntaxCombination(syntax))
        {
        }
    }

    public abstract class Stream : ISyntaxPart
    {
        public string Name { get; }
        public SyntaxList ContentList { get; }
        public Token[] StopTokens { get; }

        public Stream(
            string name,
            SyntaxList contentList,
            Token[] stopTokens)
        {
            this.Name = name;
            this.ContentList = contentList;
            this.StopTokens = stopTokens;
        }
    }

    public class SyntaxCombination
    {
        public ISyntaxPart[] Parts { get; }

        public SyntaxCombination(params ISyntaxPart[] parts)
        {
            this.Parts = parts;
        }
    }

    //public class EagerLiteral : ISyntaxPart
    //{
        
    //}

    public abstract class DelimitedSyntax : ISyntaxPart
    {
        public string Name { get; }
        public Token StartingDelimiter { get; }
        public Token EndingDelimiter { get; }
        public Token EscapeCharacter { get; }


        public DelimitedSyntax(
            string name,
            Token startingDelimiter,
            Token endingDelimiter)
        {
            this.Name = name;
            this.StartingDelimiter = startingDelimiter;
            this.EndingDelimiter = endingDelimiter;
        }

        public DelimitedSyntax(
            string name,
            Token startingDelimiter,
            Token endingDelimiter,
            Token escapeCharachter
            ) : this(name, startingDelimiter, endingDelimiter)
        {
            this.EscapeCharacter = escapeCharachter;
        }
    }

    [Flags]
    public enum SyntaxListFlags
    {
        None = 0,
        SkipParserGeneration = 1 << 0
    }

    public abstract class SyntaxList : ISyntaxPart
    {
        public ISyntaxPart[] Syntax { get; }
        public string Name { get; }
        public SyntaxListFlags Flags { get; }

        public SyntaxList(string name, ISyntaxPart[] syntax, SyntaxListFlags flags = SyntaxListFlags.None)
        {
            this.Syntax = syntax;
            this.Name = name;
            this.Flags = flags;
        }
    }

    [Flags]
    public enum DelimitedTextSyntaxFlags
    {
        None = 0,
        GenerateDelimitationParseEvent = 1 << 0,
        GenerateStreamParseEvent = 1 << 1
    }

    public abstract class DelimitedTextSyntax : ISyntaxPart
    {
        public string Name { get; }
        public Stream Stream { get; }
        public Syntax[] Delimitations { get; }
        public Syntax[] FollowedBy { get; }
        public DelimitedTextSyntaxFlags Flags { get; }

        public DelimitedTextSyntax(
            string name,
            Stream stream,
            Syntax[] delimitations,
            DelimitedTextSyntaxFlags flags = DelimitedTextSyntaxFlags.None)
        {
            this.Name = name;
            this.Stream = stream;
            this.Delimitations = delimitations;
            this.Flags = flags;
        }

        public DelimitedTextSyntax(
            string name,
            Stream stream,
            Syntax[] delimitations,
            Syntax[] followedBy,
            DelimitedTextSyntaxFlags flags) :
            this (name, stream, delimitations, flags)
        {
            this.FollowedBy = followedBy;
        }
    }

    public class StringLiteralTextToken : DynamicToken
    {
        public StringLiteralTextToken(string name) :
            base(name)
        { }
    }

    public class StringLiteralTextList : SyntaxList
    {
        public StringLiteralTextList(string name) :
            base(
                name,
                new ISyntaxPart[] {
                    new StringLiteralTextToken("TextToken")
                },
                SyntaxListFlags.SkipParserGeneration)
        { }
    }

    public class StringLiteralText : Stream
    {
        public StringLiteralText(string name) :
            base(
                name,
                new StringLiteralTextList("Text"),
                new Token[] {
                    new StringDelimiterToken("EndToken")
                })
        { }
    }

    public class StringLiteral : Syntax
    {
        public StringLiteral(string name) :
            base(
                name,
                new StringDelimiterToken("StartToken"),
                new StringLiteralText("Text"),
                new StringDelimiterToken("EndToken"))
        { }
    }

    public class StringDelimiterToken : Token
    {
        public StringDelimiterToken(string name) : base(name, "\\\"") { }
    }

    public class IdentifierToken : DynamicToken
    {
        public IdentifierToken(string name) : 
            base(
                name, 
                DynamicTokenFlags.CustomParse)
        { }
    }

    public class ControlBlockTagOpenToken : Token
    {
        public ControlBlockTagOpenToken(string name) : base(name, "<#") { }
    }

    public class ClassFeatureBlockOpenTagToken : Token
    {
        public ClassFeatureBlockOpenTagToken(string name) : base(name, "<#=") { }
    }

    public class DirectiveBlockTagOpenToken : Token
    {
        public DirectiveBlockTagOpenToken(string name) : base(name, "<#@") { }
    }

    public class EqualsToken : Token
    {
        public EqualsToken(string name) : base(name, "=") { }
    }

    public class BlockTagCloseToken : Token
    {
        public BlockTagCloseToken(string name) : base(name, "#>") { }
    }

    public class SourceCodeToken : DynamicToken
    {
        public SourceCodeToken(string name) :
            base(name)
        { }
    }

    public class TemplateTextToken : DynamicToken
    {
        public TemplateTextToken(string name) :
            base(name)
        { }
    }

    public class DirectiveNameSyntax : Syntax
    {
        public DirectiveNameSyntax(string name) :
            base(
                name,
                new IdentifierToken("IdentifierToken"))
        { }
    }

    public class DirectiveParameterIdentifierSyntax : Syntax
    {
        public DirectiveParameterIdentifierSyntax(string name) :
            base(
                name,
                new IdentifierToken("ParameterIdentifierToken"))
        { }
    }

    public class DirectiveParameterSyntax : Syntax
    {
        public DirectiveParameterSyntax(string name) : 
            base(
                name,
                new IdentifierToken("Identifier"),
                new EqualsToken("EqualsToken"),
                new StringLiteral("ParameterValue"))
        {
        }
    }

    public class DirectiveParameterSyntaxList : SyntaxList
    {
        public DirectiveParameterSyntaxList(string name) : 
            base(
                name,
                new[] {
                new DirectiveParameterSyntax("Directives")
                })
        { }
    }

    public class DirectiveContentsSyntax : Syntax
    {
        public DirectiveContentsSyntax(string name) :
            base(
                name,
                new DirectiveNameSyntax("Identifier"),
                new DirectiveParameterSyntaxList("DirectiveParameters"))
        { }
    }

    public class DirectiveSyntax : Syntax
    {
        public DirectiveSyntax() :
            base(
                "Directive",
                new DirectiveBlockTagOpenToken("StartToken"),
                new DirectiveContentsSyntax("Contents"),
                new BlockTagCloseToken("EndToken"))
        { }
    }

    public class ControlBlockStreamList : SyntaxList
    {
        public ControlBlockStreamList() :
            base(
                "ControlBlockStreamList",
                new ISyntaxPart[]
                {
                    new SourceCodeToken("SourceCodeToken")
                },
                SyntaxListFlags.SkipParserGeneration)
        { }
    }

    public class ControlBlockStream : Stream
    {
        public ControlBlockStream(string name) :
            base(
                name,
                new ControlBlockStreamList(),
                new Token[] {
                    new BlockTagCloseToken("EndToken")
                })
        { }
    }

    public class ControlBlock : Syntax
    {
        public ControlBlock() :
            base(
                "ControlBlock",
                new ControlBlockTagOpenToken("StartToken"),
                new ControlBlockStream("Contents"),
                new BlockTagCloseToken("EndToken")
                )
        { }
    }

    public class ClassFeatureBlock : Syntax
    {
        public ClassFeatureBlock(string name) :
            base(
                name,
                new ClassFeatureBlockOpenTagToken("StartToken"),
                new ControlBlockStream("Contents"),
                new BlockTagCloseToken("EndToken"))
        { }
    }

    public class StaticTextSyntaxList : SyntaxList
    {
        public StaticTextSyntaxList() :
            base(
                "StaticTextSyntaxList",
                new ISyntaxPart[] {
                    new TemplateTextToken("TextToken")
                },
                SyntaxListFlags.SkipParserGeneration)
        { }
    }

    public class StaticTextSyntax : Stream
    {
        public StaticTextSyntax(string name) :
            base(
                name, 
                new StaticTextSyntaxList(),
                new[] {
                    new ControlBlockTagOpenToken("ControlBlockStartToken")
                })
        { }
    }

    public class DirectiveSyntaxList : SyntaxList
    {
        public DirectiveSyntaxList(string name) :
            base(
                name,
                new[] {
                    new DirectiveSyntax()
                })
        { }
    }

    public class TemplateBodySyntaxList : SyntaxList
    {
        public TemplateBodySyntaxList() :
            base(
                "TemplateBodySyntax",
                new ISyntaxPart [] {
                    new StaticTextSyntax("Text"),
                    new ControlBlock()
                },
                SyntaxListFlags.SkipParserGeneration)
        { }
    }

    public class TemplateBodySyntax : DelimitedTextSyntax
    {
        public TemplateBodySyntax() :
            base(
                "TemplateBody",
                new StaticTextSyntax("Text"),
                new Syntax[] {
                    new ControlBlock(),
                    new DirectiveSyntax()
                },
                new Syntax[] {
                    new ClassFeatureBlock(nameof(ClassFeatureBlock))
                },
                DelimitedTextSyntaxFlags.GenerateDelimitationParseEvent)
        { }
    }

    public class TemplateSyntax : Syntax
    {
        public TemplateSyntax() :
            base(
                "Template",
                new SyntaxCombination(
                    new DirectiveSyntaxList("Directives"),
                    new TemplateBodySyntax()),
                new SyntaxCombination())
        { }
    }
}
