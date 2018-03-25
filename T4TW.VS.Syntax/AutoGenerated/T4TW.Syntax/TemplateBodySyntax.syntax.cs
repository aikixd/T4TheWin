
using Microsoft.VisualStudio.Text;

namespace T4TW.Syntax
{
	public partial class TemplateBodySyntax : ISyntaxNode
	{
		public Span Span { get; }


		public TemplateBodySyntaxCollection TemplateBodySyntaxCollection { get; }
			
	}
}