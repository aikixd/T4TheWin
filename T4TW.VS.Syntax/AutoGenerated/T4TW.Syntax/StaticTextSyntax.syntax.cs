
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class StaticTextSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public StaticTextSyntaxTokens Tokens { get; }

			
	}
}