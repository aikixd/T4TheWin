﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// _CodeGenerator.Definitions.Syntax.Syntax
	public partial class DirectiveNameSyntax : ISyntaxNode
	{
		public Span Span { get; }
		/***** Syntax *****/
			
		public IdentifierToken IdentifierToken { get; }

			
		public DirectiveNameSyntax(IdentifierToken IdentifierToken)
		{

				this.IdentifierToken = IdentifierToken;
						
		}

			
	}
}