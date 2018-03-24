using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Definitions.Syntax
{
    public interface ISyntaxPart
    {
    }

    public class Whitespace : ISyntaxPart
    {
        public char[] Chars =>
            new char[]
            {
                ' ',
                '\t',
                '\r',
                '\n'
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
        private SyntaxCombination[] combinations;

        public Syntax(params SyntaxCombination[] combinations)
        {
            this.combinations = combinations;
        }

        public Syntax(params ISyntaxPart[] syntax)
            : this(new SyntaxCombination(syntax))
        {
        }
    }

    public class SyntaxCombination
    {
        private ISyntaxPart[] parts;

        public SyntaxCombination(params ISyntaxPart[] parts)
        {
            this.parts = parts;
        }
    }
    
    public class EagerLiteral : ISyntaxPart
    {

    }

    public class DelimitedLiteral : ISyntaxPart
    {
        public Token StartingDelimiter { get; }
        public Token EndingDelimiter { get; }
        public Token EscapeCharacter { get; }

        public DelimitedLiteral(
                Token startingDelimiter,
                Token endingDelimiter)
        {
            this.StartingDelimiter = startingDelimiter;
            this.EndingDelimiter = endingDelimiter;
        }

        public DelimitedLiteral(
            Token startingDelimiter,
            Token endingDelimiter,
            Token escapeCharachter
            ) : this(startingDelimiter, endingDelimiter)
        {
            this.EscapeCharacter = escapeCharachter;
        }
    }

    public class StringLiteral : DelimitedLiteral
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

    public class StaticTextLiteral : EagerLiteral
    {
        public StaticTextLiteral() :
            base(
                )
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

    public class SyntaxList<T> : ISyntaxPart
        where T : Syntax, new()
    {
        public T Syntax { get; }

        public SyntaxList()
        {
            this.Syntax = new T();
        }
    }

    public class DirectiveContentsSyntax : Syntax
    {
        public DirectiveContentsSyntax() :
            base(
                new DirectiveNameSyntax(),
                new SyntaxList<DirectiveParameterSyntax>())
        { }
    }

    public class ControlBlock : DelimitedLiteral
    {
        public ControlBlock() :
            base(
                new ControlBlockTagOpenToken(),
                new BlockTagCloseToken()
                )
        { }
    }
}
