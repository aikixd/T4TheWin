using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Definitions.Syntax
{
    public interface ISyntaxPart
    {
        
    }

    public interface IBlock { }

    public class Whitespace : ISyntaxPart
    {
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
        public string Text { get; }

        public bool Optional { get; set; }

        public Token SetOptional(bool optional)
        {
            this.Optional = optional;
            return this;
        }

        public Token(string text)
        {
            this.Text = text;
        }
    }

    public abstract class Syntax : ISyntaxPart
    {
        public SyntaxCombination[] Combinations { get; }

        public Syntax(params SyntaxCombination[] combinations)
        {
            this.Combinations = combinations;
        }

        public Syntax(params ISyntaxPart[] syntax)
            : this(new SyntaxCombination(syntax))
        {
        }
    }

    public abstract class Stream : ISyntaxPart
    {
        string[] Allowed { get; }
        string[] Disallowed { get; }

        public Stream(
            string[] allowed = null,
            string[] disallowed = null)
        {
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

    public class EagerLiteral : ISyntaxPart
    {
        
    }

    public class DelimitedSyntax : ISyntaxPart
    {
        public Token StartingDelimiter { get; }
        public Token EndingDelimiter { get; }
        public Token EscapeCharacter { get; }


        public DelimitedSyntax(
                Token startingDelimiter,
                Token endingDelimiter)
        {
            this.StartingDelimiter = startingDelimiter;
            this.EndingDelimiter = endingDelimiter;
        }

        public DelimitedSyntax(
            Token startingDelimiter,
            Token endingDelimiter,
            Token escapeCharachter
            ) : this(startingDelimiter, endingDelimiter)
        {
            this.EscapeCharacter = escapeCharachter;
        }
    }

    public abstract class SyntaxList : ISyntaxPart
    {
        public ISyntaxPart Syntax { get; }
        

        public SyntaxList(ISyntaxPart syntax)
        {
            this.Syntax = syntax;
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
        public Stream Stream { get; }
        public Syntax[] Delimitations { get; }
        public DelimitedTextSyntaxFlags Flags { get; }

        public DelimitedTextSyntax(
            Stream stream,
            Syntax[] delimitations,
            DelimitedTextSyntaxFlags flags = DelimitedTextSyntaxFlags.None)
        {            
            this.Stream = stream;
            this.Delimitations = delimitations;
            this.Flags = flags;
        }
    }

    public class StringLiteral : DelimitedSyntax
    {
        public StringLiteral() :
            base(
                new Token("\""),
                new Token("\""))
        { }
    }

    public class ControlBlockContentsLiteral : EagerLiteral
    {
    }

    

    public class IdentifierToken : Token
    {
        public IdentifierToken() : base("") { }
    }

    public class ControlBlockTagOpenToken : Token
    {
        public ControlBlockTagOpenToken() : base("<#") { }
    }

    public class DirectiveBlockTagOpenToken : Token
    {
        public DirectiveBlockTagOpenToken() : base("<#@") { }
    }

    public class EqualsToken : Token
    {
        public EqualsToken() : base("=") { }
    }

    public class BlockTagCloseToken : Token
    {
        public BlockTagCloseToken() : base("#>") { }
    }

    public class DirectiveNameSyntax : Syntax
    {
        public DirectiveNameSyntax() :
            base(
                new IdentifierToken())
        { }
    }

    public class DirectiveParameterIdentifierSyntax : Syntax
    {
        public DirectiveParameterIdentifierSyntax() :
            base(
                new IdentifierToken())
        { }
    }

    public class DirectiveParameterSyntax : Syntax
    {
        public DirectiveParameterSyntax() : 
            base(
                new DirectiveParameterIdentifierSyntax(),
                new EqualsToken(),
                new StringLiteral())
        {
        }
    }

    public class DirectiveParameterSyntaxList : SyntaxList
    {
        public DirectiveParameterSyntaxList() : 
            base(
                new DirectiveParameterSyntax())
        { }
    }

    public class DirectiveContentsSyntax : Syntax
    {
        public DirectiveContentsSyntax() :
            base(
                new DirectiveNameSyntax(),
                new DirectiveParameterSyntaxList())
        { }
    }

    public class DirectiveSyntax : Syntax
    {
        public DirectiveSyntax() :
            base(
                new DirectiveBlockTagOpenToken(),
                new DirectiveContentsSyntax(),
                new BlockTagCloseToken())
        { }
    }

    public class ControlBlockStream : Stream
    {
        public ControlBlockStream() :
            base()
        { }
    }

    public class ControlBlock : Syntax
    {
        public ControlBlock() :
            base(
                new ControlBlockTagOpenToken(),
                new ControlBlockStream(),
                new BlockTagCloseToken()
                )
        { }
    }

    public class StaticTextLiteral : Stream
    {
        public StaticTextLiteral() :
            base()
        { }
    }

    public class DirectiveSyntaxList : SyntaxList
    {
        public DirectiveSyntaxList() :
            base(
                new DirectiveSyntax())
        { }
    }

    public class TemplateBodySyntax : DelimitedTextSyntax
    {
        public TemplateBodySyntax() :
            base(
                new StaticTextLiteral(),
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
                new SyntaxCombination(
                    new DirectiveSyntaxList(),
                    new TemplateBodySyntax()),
                new SyntaxCombination())
        { }
    }
}
