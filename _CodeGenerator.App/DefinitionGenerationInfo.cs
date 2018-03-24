using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App
{
    internal class DefinitionGenerationInfo
    {
        public string Name { get; }
        public string Namespace { get; }
        public string Affix { get; }
        public string Output { get; }

        public DefinitionGenerationInfo(
            string name,
            string @namespace,
            string affix,
            string output)
        {
            this.Name = name;
            this.Namespace = @namespace;
            this.Affix = affix;
            this.Output = output;
        }
    }
}
