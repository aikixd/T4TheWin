﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// _CodeGenerator.Definitions.Syntax.Stream
	public partial class StaticTextSyntax : ISyntaxNode
	{
		public Span Span { get; }
		/***** Stream *****/
		public StaticTextSyntaxTokens Tokens { get; }

		public StaticTextSyntax(IList<RawToken> tokens)
		{
			this.Tokens = new StaticTextSyntaxTokens(tokens);
			this.Span = this.Tokens.Span;
		}

			
	}
}