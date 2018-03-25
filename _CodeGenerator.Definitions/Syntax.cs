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

    public class Token : ISyntaxPart
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
        string[] Allowed { get; }
        string[] Disallowed { get; }

        public Stream(
            string name,
            string[] allowed = null,
            string[] disallowed = null)
        {
            this.Name = name;
            this.Allowed = allowed;
            this.Disallowed = disallowed;
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

    public class DelimitedSyntax : ISyntaxPart
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

    public abstract class SyntaxList : ISyntaxPart
    {
        public ISyntaxPart Syntax { get; }
        public string Name { get; }
        

        public SyntaxList(ISyntaxPart syntax, string name)
        {
            this.Syntax = syntax;
            this.Name = name;
        }
    }

    [Flags]
    public enum DelimitedTextSyntaxFlags
    {
        None = 0,
        GenerateDelimitationParseEvent = 1 << 0,
        GenerateStreamParseEvent = 1 << 1
    }

    public class DelimitedTextSyntax : ISyntaxPart
    {
        public string Name { get; }
        public Stream Stream { get; }
        public Syntax[] Delimitations { get; }
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
    }

    public class StringLiteral : DelimitedSyntax
    {
        public StringLiteral(string name) :
            base(
                name,
                new Token("StartToken", "\""),
                new Token("EndToken", "\""))
        { }
    }

    //public class ControlBlockContentsLiteral : EagerLiteral
    //{
    //}

    

    public class IdentifierToken : Token
    {
        public IdentifierToken(string name) : base(name, "") { }
    }

    public class ControlBlockTagOpenToken : Token
    {
        public ControlBlockTagOpenToken(string name) : base(name, "<#") { }
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
                new DirectiveParameterIdentifierSyntax("Identifier"),
                new EqualsToken("EqualsToken"),
                new StringLiteral("ParameterValue"))
        {
        }
    }

    public class DirectiveParameterSyntaxList : SyntaxList
    {
        public DirectiveParameterSyntaxList(string name) : 
            base(
                new DirectiveParameterSyntax("Directives"), name)
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

    public class ControlBlockStream : Stream
    {
        public ControlBlockStream(string name) :
            base(name)
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

    public class StaticTextLiteral : Stream
    {
        public StaticTextLiteral(string name) :
            base(name)
        { }
    }

    public class DirectiveSyntaxList : SyntaxList
    {
        public DirectiveSyntaxList(string name) :
            base(
                new DirectiveSyntax(), name)
        { }
    }

    public class TemplateBodySyntax : DelimitedTextSyntax
    {
        public TemplateBodySyntax() :
            base(
                "TemplateBody",
                new StaticTextLiteral("Text"),
                new Syntax[] {
                    new ControlBlock()
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
