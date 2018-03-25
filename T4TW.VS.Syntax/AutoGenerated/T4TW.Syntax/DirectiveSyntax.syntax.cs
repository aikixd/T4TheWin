
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class DirectiveSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public DirectiveBlockTagOpenToken StartToken { get; }

			
		public DirectiveContentsSyntax Contents { get; }

			
		public BlockTagCloseToken EndToken { get; }

			
		public DirectiveSyntax(DirectiveBlockTagOpenToken StartToken, DirectiveContentsSyntax Contents, BlockTagCloseToken EndToken)
		{

				this.StartToken = StartToken;
				
				this.Contents = Contents;
				
				this.EndToken = EndToken;
						
		}

			
	}
}