﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class StaticTextSyntaxTokens
	{
		private RawToken[] tokens;
		
		public Span Span { get; }
		
		public StaticTextSyntaxTokens(IList<RawToken> tokens)
		{
			if (tokens.Count == 0)
				throw new ArgumentException($"Received an empty token list. The list should not be empty, sinse the span is calculated from the tokens.");
			
			var spanStart = tokens[0].Span.Start;
			var spanEnd = tokens[tokens.Count - 1].Span.End;
			var spanLen = spanEnd - spanStart;
			
			this.Span = new Span(spanStart, spanEnd);
			this.tokens = tokens.ToArray();
		}
	}
}