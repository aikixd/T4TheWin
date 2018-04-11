using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace T4TW.Syntax
{
    public partial class Parser
    {
        private bool TryParseIdentifierTokenImpl(Lexer lexer, string[] stopSignals, out IdentifierToken result)
        {
            var next = lexer.Next();
            int newLength = 0;

            for (int i = 0; i < next.Span.Length; i += 1)
            {
                var curChar = next.Text[i];

                if (curChar >= 'A' && curChar <= 'Z' ||
                    curChar >= 'a' && curChar <= 'z' == false)
                {
                    newLength = i + 1;
                    break;
                }

                result = new IdentifierToken(next);
                return true;
            }

            if (newLength == 0)
            {
                result = null;
                return false;
            }

            result = new IdentifierToken(
                new RawToken(
                    next.Text.Substring(0, newLength), 
                    new Span(next.Span.Start, newLength)));

            return true;
        }
    }
}
