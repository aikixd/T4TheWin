﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// SyntaxList
	public partial class StringLiteralTextList : ISyntaxNode
	{
		public Span Span { get; }
		/***** SyntaxList *****/

		private IStringLiteralTextListContent[] content;
		
		public StringLiteralTextList(IList<IStringLiteralTextListContent> content)
		{
			this.content = content.ToArray();
		}

			
	}
}