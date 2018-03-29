﻿
// Generated by: SyntaxNodeTemplate.tt

using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;


namespace T4TW.Syntax
{
	// _CodeGenerator.Definitions.Syntax.Syntax
	public partial class ClassFeatureBlock : ISyntaxNode
	{
		public Span Span { get; }
		/***** Syntax *****/
			
		public ClassFeatureBlockOpenTagToken StartToken { get; }

			
		public ControlBlockStream Contents { get; }

			
		public BlockTagCloseToken EndToken { get; }

			
		public ClassFeatureBlock(ClassFeatureBlockOpenTagToken StartToken, ControlBlockStream Contents, BlockTagCloseToken EndToken)
		{

				this.StartToken = StartToken;
				
				this.Contents = Contents;
				
				this.EndToken = EndToken;
						
		}

			
	}
}