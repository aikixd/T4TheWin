﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// SyntaxList
	public partial class StaticTextSyntaxList : ISyntaxNode
	{
		public Span Span { get; }
		/***** SyntaxList *****/

		private IStaticTextSyntaxListContent[] content;
		
		public StaticTextSyntaxList(IList<IStaticTextSyntaxListContent> content)
		{
			this.content = content.ToArray();
		}

			
	}
}