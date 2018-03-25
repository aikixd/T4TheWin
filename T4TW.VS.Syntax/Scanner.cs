using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4TW.Syntax;

namespace T4TW.Syntax
{
    public struct ScannerResult
    {
        public CharachterType CharachterType;
        public char Charachter;
        public int Position;

        public ScannerResult(CharachterType charachterType, char chr, int position)
        {
            this.CharachterType = charachterType;
            this.Charachter = chr;
            this.Position = position;
        }
    }

    public enum CharachterType
    {
        Regular,
        Eof,
        Whitespace
    }

    public partial class Scanner
    {
        private int position;
        private string text;

        public Scanner Advance()
        {
            this.position += 1;
            return this;
        }

        public ScannerResult Peek()
        {
            var pos = this.position + 1;

            if (pos >= this.text.Length)
                return new ScannerResult(CharachterType.Eof, '\0', pos);

            var chr = this.text[pos];

            return new ScannerResult(
                this.IsWhitespaceChar(chr) ? CharachterType.Whitespace : CharachterType.Regular,
                chr,
                pos);
        }

        public string Substring(int position, int length)
        {
            if (position + length > this.text.Length)
                return this.text.Substring(position);

            return this.text.Substring(position, length);
        }

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
