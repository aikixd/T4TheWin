﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// SyntaxList
	public partial class DirectiveParameterSyntaxList : ISyntaxNode
	{
		public Span Span { get; }
		/***** SyntaxList *****/

		private IDirectiveParameterSyntaxListContent[] content;
		
		public DirectiveParameterSyntaxList(IList<IDirectiveParameterSyntaxListContent> content)
		{
			this.content = content.ToArray();
		}

			
	}
}