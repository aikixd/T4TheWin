﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// SyntaxList
	public partial class ControlBlockStreamList : ISyntaxNode
	{
		public Span Span { get; }
		/***** SyntaxList *****/

		private IControlBlockStreamListContent[] content;
		
		public ControlBlockStreamList(IList<IControlBlockStreamListContent> content)
		{
			this.content = content.ToArray();
		}

			
	}
}