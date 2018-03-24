using Microsoft.VisualStudio.Text;
using System;

namespace T4TW.Syntax
{
    public class RawToken
    {
        public Span Span { get; }
        public string Text { get; }

        public RawToken(string text, Span span)
        {
            this.Span = span;
            this.Text = text;
        }
    }
}
