using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4TW.Syntax;

namespace T4TW.Syntax
{
    public partial class Scanner
    {
        public RawTokenCollection Scan(string text)
        {
            var col = new RawTokenCollection();

            for (int i = 0; i < text.Length; i += 1)
            {
                if (this.IsWhitespaceChar(text[i]))
                    continue;

                var start = i;
                var end = i;

                while (
                    i < text.Length &&
                    this.IsWhitespaceChar(text[i]) == false)
                {
                    i += 1;
                    end = i;
                }

                col.Append(
                    new RawToken(
                        text.Substring(start, end - start), 
                        new Span(start, end - start)));
            }

            return col;
        }
    }
}
