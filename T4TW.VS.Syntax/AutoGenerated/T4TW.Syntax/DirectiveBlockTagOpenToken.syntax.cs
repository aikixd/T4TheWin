﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// Token
	public partial class DirectiveBlockTagOpenToken : ISyntaxNode
	{
		public Span Span { get; }

		public string Text { get; }

		public DirectiveBlockTagOpenToken(RawToken rt)
		{
			this.Span = rt.Span;
			this.Text = rt.Text;
		}

			
	}
}