﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// DynamicToken
	public partial class TemplateTextToken : ISyntaxNode
	{
		public Span Span { get; }

		public string Text { get; }

		public TemplateTextToken(RawToken rt)
		{
			this.Span = rt.Span;
			this.Text = rt.Text;
		}

			
	}
}