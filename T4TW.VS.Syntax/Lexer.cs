using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace T4TW.Syntax
{
    public class Lexer
    {
        private readonly Scanner scanner;

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
                isStopSign() == false)
            {
                length += 1;
                scannerResult = this.scanner.Advance().Peek();
            }

            return new RawToken(this.scanner.Substring(start, length), new Span(start, length));

            /***** Local functions *****/

            bool isStopSign()
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
                            this.scanner.Substring(
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
