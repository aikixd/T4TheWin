using System;
using System.Collections.Generic;
using System.Text;
using T4TW.Syntax;

namespace T4TW.Syntax
{
    public class RawTokenCollection
    {
        private LinkedList<RawToken> tokens;

        public RawTokenCollection()
        {
            this.tokens = new LinkedList<RawToken>();
        }

        public void Append(RawToken token)
        {
            this.tokens.AddLast(token);
        }

        
    }
}
