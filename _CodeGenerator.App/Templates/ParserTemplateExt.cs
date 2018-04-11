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

        public string MakeStringArrayInit(IEnumerable<string> list)
        {
            var str =
                list != null ?
                    string.Join(
                        ", ",
                        list.Select(x => "\"" + x + "\"")) :
                    "";
            return $"new string[] {{{ str }}}";
        }

        private Argument[] GetArguments(ISyntaxPart part)
        {
            var list = new LinkedList<Argument>();

            list.AddLast(
                new Argument(
                    "lexer",
                    new Domain.Type("Lexer", this.ClassInfo.Namespace)));

            if (part is DynamicToken)
                list.AddLast(
                    new Argument(
                        "stopSignals",
                        new Domain.Type("string[]", "System")));

            return list.ToArray();
        }

        private string MakeTryParseCall(ISyntaxPart fromPart, ISyntaxPart calledPart, string outName)
        {
            var args = new LinkedList<string>();

            args.AddLast("lexer");

            if (calledPart is DynamicToken)
            {
                if (fromPart is Stream s)
                {
                    args.AddLast(this.MakeStringArrayInit(s.StopTokens.Select(x => x.Text)));
                }

                else
                {
                    args.AddLast("new string[] { }");
                }
            }

            args.AddLast($"out var {outName}");

            return $"this.TryParse{calledPart.GetType().Name}({string.Join(", ", args.Select(x => x))})";
        }

        private string MakeParameters(Argument[] args)
        {
            return string.Join(
                ", ",
                args
                .Select(x => $"{x.Type.Name} {x.Name}"));
        }

        private string MakeArguments(Argument[] args)
        {
            return string.Join(
                ", ",
                args
                .Select(x => $"{x.Name}"));
        }
    }
}
