
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class DirectiveContentsSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public DirectiveNameSyntax Identifier { get; }

			
		public DirectiveParameterSyntaxList DirectiveParameters { get; }

			
		public DirectiveContentsSyntax(DirectiveNameSyntax Identifier, DirectiveParameterSyntaxList DirectiveParameters)
		{

				this.Identifier = Identifier;
				
				this.DirectiveParameters = DirectiveParameters;
						
		}

			
	}
}