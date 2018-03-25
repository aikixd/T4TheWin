
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class DirectiveNameSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public IdentifierToken IdentifierToken { get; }

			
		public DirectiveNameSyntax(IdentifierToken IdentifierToken)
		{

				this.IdentifierToken = IdentifierToken;
						
		}

			
	}
}