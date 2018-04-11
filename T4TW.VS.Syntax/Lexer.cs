using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace T4TW.Syntax
{
    public class Lexer
    {
        private readonly Scanner scanner;

        public Lexer(string text)
        {
            this.scanner = new Scanner(text);
        }

        public bool CanRead()
        {
            var peeked = this.scanner.Peek();

            return peeked.CharachterType != CharachterType.Eof;
        }

        public bool CanReadExcept(params string[] stopSigns)
        {
            var next = this.Next(stopSigns);

            if (next.Span.Length == 0)
                return false;

            return true;
        }

        public RawToken Next(params string[] stopSigns)
        {
            ScannerResult scannerResult = this.scanner.Peek();

            // trim whitespace
            while (scannerResult.CharachterType != CharachterType.Regular)
            {
                if (scannerResult.CharachterType == CharachterType.Eof)
                    return null;

                scannerResult = this.scanner.Advance().Peek();
            }

            int start = scannerResult.Position;
            int length = 0;

            while (
                scannerResult.CharachterType == CharachterType.Regular &&
                isStopSign(this.scanner) == false)
            {
                length += 1;
                scannerResult = this.scanner.Advance().Peek();
            }

            if (length == 0)
                return null;

            return new RawToken(this.scanner.Substring(start, length), new Span(start, length));

            /***** Local functions *****/

            bool isStopSign(Scanner scanner)
            {
                foreach (var sign in stopSigns)
                {
                    if (sign.Length == 0)
                        return true;

                    if (scannerResult.Charachter == sign[0])
                    {
                        if (sign.Length == 1)
                            return true;

                        if (
                            scanner.Substring(
                                scannerResult.Position,
                                sign.Length) == sign)
                            return true;
                    }
                }

                return false;
            }
        }
    }
}
