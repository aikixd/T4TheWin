
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace T4TW.Vsix
{
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template")]
    [Name("T4Template")]
    [UserVisible(false)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplateFormat : ClassificationFormatDefinition
    {
        public T4TemplateFormat()
        {
            this.DisplayName = "T4Template";
        }
    }
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template.control")]
    [Name("T4Template.control")]
    [UserVisible(true)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplatecontrolFormat : ClassificationFormatDefinition
    {
        public T4TemplatecontrolFormat()
        {
            this.DisplayName = "T4Template.control";
        }
    }
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template.control.delimiter")]
    [Name("T4Template.control.delimiter")]
    [UserVisible(true)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplatecontroldelimiterFormat : ClassificationFormatDefinition
    {
        public T4TemplatecontroldelimiterFormat()
        {
            this.DisplayName = "T4Template.control.delimiter";
			this.BackgroundColor = Colors.Cyan;
        }
    }
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template.control.delimiter.start")]
    [Name("T4Template.control.delimiter.start")]
    [UserVisible(false)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplatecontroldelimiterstartFormat : ClassificationFormatDefinition
    {
        public T4TemplatecontroldelimiterstartFormat()
        {
            this.DisplayName = "T4Template.control.delimiter.start";
        }
    }
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template.control.delimiter.end")]
    [Name("T4Template.control.delimiter.end")]
    [UserVisible(false)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplatecontroldelimiterendFormat : ClassificationFormatDefinition
    {
        public T4TemplatecontroldelimiterendFormat()
        {
            this.DisplayName = "T4Template.control.delimiter.end";
        }
    }
	
	[Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "T4Template.static")]
    [Name("T4Template.static")]
    [UserVisible(true)] 
    [Order(Before = Priority.Default)] 
    internal sealed class T4TemplatestaticFormat : ClassificationFormatDefinition
    {
        public T4TemplatestaticFormat()
        {
            this.DisplayName = "T4Template.static";
        }
    }
}