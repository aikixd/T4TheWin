using System;
using System.Collections.Generic;
using System.Text;

namespace T4TW.Syntax
{
    partial class Scanner
    {
        public RawTokenCollection Scan(string text)
        {
            for (int i = 0; i < text.Length; i += 1)
            {
                if (this.Whitespace.Contains(text[i]))
                    continue;

                var start = i;
            }
        }
    }
}
