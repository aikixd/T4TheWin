
using System.Collections.Generic;

namespace T4TW.Syntax
{
	partial class Parser
	{
		private readonly Lexer lexer;
		public bool TryParseTemplateSyntax(out TemplateSyntax result)
		{
			if (
				this.TryParseDirectiveSyntaxList(out var syntaxPart0) &&
				this.TryParseTemplateBodySyntax(out var syntaxPart1))
			{
				result = new TemplateSyntax(syntaxPart0, syntaxPart1);
				return true;
			}

			if (
                true)
			{
				result = new TemplateSyntax();
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseTemplateBodySyntax(out TemplateBodySyntax result)
		{

			var syntaxList = new LinkedList<ISyntaxNode>();
			var streamList = new LinkedList<RawToken>();

			while (/* while not end of file, and not disallowed symbol */)
			{

				if (this.TryParseControlBlock(out var r))
				{
					syntaxList.AddLast(new StaticTextLiteral());
					syntaxList.AddLast(r);
					continue;
				}

				streamList.AddLast(this.lexer.Next());

			}

			syntaxList.AddLast(new StaticTextLiteral());

			result = new TemplateBodySyntax(syntaxList);
						
		} // Parse method end
		public bool TryParseDirectiveSyntaxList(out DirectiveSyntaxList result)
		{

			var list = new LinkedList<SyntaxNode>();

			while (this.TryParseDirectiveSyntax(out var r))
			{
				list.AddLast(r);
			}

			result = new DirectiveSyntaxList(list);

						
		} // Parse method end
		public bool TryParseStaticTextLiteral(out StaticTextLiteral result)
		{
		
		} // Parse method end
		public bool TryParseControlBlock(out ControlBlock result)
		{
			if (
				this.TryParseControlBlockTagOpenToken(out var syntaxPart0) &&
				this.TryParseControlBlockStream(out var syntaxPart1) &&
				this.TryParseBlockTagCloseToken(out var syntaxPart2))
			{
				result = new ControlBlock(syntaxPart0, syntaxPart1, syntaxPart2);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseDirectiveSyntax(out DirectiveSyntax result)
		{
			if (
				this.TryParseDirectiveBlockTagOpenToken(out var syntaxPart0) &&
				this.TryParseDirectiveContentsSyntax(out var syntaxPart1) &&
				this.TryParseBlockTagCloseToken(out var syntaxPart2))
			{
				result = new DirectiveSyntax(syntaxPart0, syntaxPart1, syntaxPart2);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseDirectiveContentsSyntax(out DirectiveContentsSyntax result)
		{
			if (
				this.TryParseDirectiveNameSyntax(out var syntaxPart0) &&
				this.TryParseDirectiveParameterSyntaxList(out var syntaxPart1))
			{
				result = new DirectiveContentsSyntax(syntaxPart0, syntaxPart1);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseDirectiveParameterSyntaxList(out DirectiveParameterSyntaxList result)
		{

			var list = new LinkedList<SyntaxNode>();

			while (this.TryParseDirectiveParameterSyntax(out var r))
			{
				list.AddLast(r);
			}

			result = new DirectiveParameterSyntaxList(list);

						
		} // Parse method end
		public bool TryParseDirectiveParameterSyntax(out DirectiveParameterSyntax result)
		{
			if (
				this.TryParseDirectiveParameterIdentifierSyntax(out var syntaxPart0) &&
				this.TryParseEqualsToken(out var syntaxPart1) &&
				this.TryParseStringLiteral(out var syntaxPart2))
			{
				result = new DirectiveParameterSyntax(syntaxPart0, syntaxPart1, syntaxPart2);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseDirectiveParameterIdentifierSyntax(out DirectiveParameterIdentifierSyntax result)
		{
			if (
				this.TryParseIdentifierToken(out var syntaxPart0))
			{
				result = new DirectiveParameterIdentifierSyntax(syntaxPart0);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
		public bool TryParseDirectiveNameSyntax(out DirectiveNameSyntax result)
		{
			if (
				this.TryParseIdentifierToken(out var syntaxPart0))
			{
				result = new DirectiveNameSyntax(syntaxPart0);
				return true;
			}

			result = null;
			return false;
						
		} // Parse method end
	}
}