using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace T4TW.VS.Syntax.Tests
{
    [TestClass]
    public class Scanner
    {
        [TestMethod]
        public void SimpleString()
        {
            var scanner = new T4TW.Syntax.Scanner();

            var col = scanner.Scan(" one two<# #>three");

            throw new NotImplementedException();
        }
    }
}
