
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class ControlBlock : ISyntaxNode
	{
		public Span Span { get; }


		public ControlBlockTagOpenToken StartToken { get; }

			
		public ControlBlockStream Contents { get; }

			
		public BlockTagCloseToken EndToken { get; }

			
		public ControlBlock(ControlBlockTagOpenToken StartToken, ControlBlockStream Contents, BlockTagCloseToken EndToken)
		{

				this.StartToken = StartToken;
				
				this.Contents = Contents;
				
				this.EndToken = EndToken;
						
		}

			
	}
}