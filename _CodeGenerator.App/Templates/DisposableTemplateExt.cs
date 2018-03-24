using _CodeGenerator.Bridge;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class DisposableTemplate
    {
        public IClassInfoProvider Class { get; }

        public DisposableTemplate(IClassInfoProvider classInfo)
        {
            this.Class = classInfo;
        }

        private DisposableOption GetOption()
        {
            var args =
                this
                .Class
                .Attributes
                .First(x => x.Type.IsSameType(typeof(GenerateDisposableAttribute)))
                .Arguments;

            if (args.Any() == false)
                return DisposableOption.Both;

            return
                args
                .Select(x => (DisposableOption)x.Value)
                .Single();
        }
    }
}
