﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// Stream
	public partial class StringLiteralText : ISyntaxNode
	{
		public Span Span { get; }
		/***** Stream *****/
		public StringLiteralTextTokens Tokens { get; }

		public StringLiteralText(IList<IStringLiteralTextListContent> tokens)
		{
			this.Tokens = new StringLiteralTextTokens(tokens);
			this.Span = this.Tokens.Span;
		}

			
	}
}