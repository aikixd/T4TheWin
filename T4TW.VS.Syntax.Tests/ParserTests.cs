using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace T4TW.VS.Syntax.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parser_Simple()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates\\Parser_Simple.tt");

            var text = File.ReadAllText(path);

            var parser = new T4TW.Syntax.Parser();

            parser.TryParseTemplateSyntax(new T4TW.Syntax.Lexer(text), out var result);

            Assert.Fail();
        }
    }
}
