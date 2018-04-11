﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// Syntax
	public partial class DirectiveContentsSyntax : ISyntaxNode
	{
		public Span Span { get; }
		/***** Syntax *****/
			
		public DirectiveNameSyntax Identifier { get; }

			
		public DirectiveParameterSyntaxList DirectiveParameters { get; }

			
		public DirectiveContentsSyntax(DirectiveNameSyntax Identifier, DirectiveParameterSyntaxList DirectiveParameters)
		{

				this.Identifier = Identifier;
				
				this.DirectiveParameters = DirectiveParameters;
						
		}

			
	}
}