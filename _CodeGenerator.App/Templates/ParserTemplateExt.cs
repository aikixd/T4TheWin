using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class ParserTemplate
    {
        public IClassInfoProvider ClassInfo { get; }
        public ISyntaxPart[] SyntaxParts { get; }

        public ParserTemplate(IClassInfoProvider classInfo, ISyntaxPart[] syntaxParts)
        {
            this.ClassInfo = classInfo;
            this.SyntaxParts = syntaxParts;
        }

        public string MakeStringList(IEnumerable<string> list)
        {
            var str =
                list != null ?
                    string.Join(
                        ", ",
                        list.Select(x => "\"" + x + "\"")) :
                    "";
            return $"new string[] {{{ str }}}";
        }
    }
}
