
using System.Linq;

namespace T4TW.Syntax
{
	partial class Scanner
	{
		private readonly char[] whitespaceChars = 
			new char[] {
				' ', '\t', '\r', '\n'
			};

		private bool IsWhitespaceChar(char chr) => this.whitespaceChars.Contains(chr);
	}
}