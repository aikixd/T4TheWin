
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class DirectiveParameterSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public DirectiveParameterIdentifierSyntax Identifier { get; }

			
		public EqualsToken EqualsToken { get; }

			
		public StringLiteral ParameterValue { get; }

			
		public DirectiveParameterSyntax(DirectiveParameterIdentifierSyntax Identifier, EqualsToken EqualsToken, StringLiteral ParameterValue)
		{

				this.Identifier = Identifier;
				
				this.EqualsToken = EqualsToken;
				
				this.ParameterValue = ParameterValue;
						
		}

			
	}
}