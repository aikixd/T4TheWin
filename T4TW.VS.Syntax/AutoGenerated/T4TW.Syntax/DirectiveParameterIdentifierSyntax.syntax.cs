
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class DirectiveParameterIdentifierSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public IdentifierToken ParameterIdentifierToken { get; }

			
		public DirectiveParameterIdentifierSyntax(IdentifierToken ParameterIdentifierToken)
		{

				this.ParameterIdentifierToken = ParameterIdentifierToken;
						
		}

			
	}
}