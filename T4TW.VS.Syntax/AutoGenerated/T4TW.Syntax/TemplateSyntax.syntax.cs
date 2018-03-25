
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class TemplateSyntax : ISyntaxNode
	{
		public Span Span { get; }


		public DirectiveSyntaxList Directives { get; }

			
		public TemplateBodySyntax TemplateBody { get; }

			
		public TemplateSyntax(DirectiveSyntaxList Directives, TemplateBodySyntax TemplateBody)
		{

				this.Directives = Directives;
				
				this.TemplateBody = TemplateBody;
						
		}

			
		public TemplateSyntax()
		{
		
		}

			
	}
}