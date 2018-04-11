using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace T4TW.Syntax
{
    public interface ISyntaxNode
    {
        Span Span { get; }
    }
}
