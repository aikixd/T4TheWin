﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// SyntaxList
	public partial class TemplateBodySyntaxList : ISyntaxNode
	{
		public Span Span { get; }
		/***** SyntaxList *****/

		private ITemplateBodySyntaxContent[] content;
		
		public TemplateBodySyntaxList(IList<ITemplateBodySyntaxContent> content)
		{
			this.content = content.ToArray();
		}

			
	}
}