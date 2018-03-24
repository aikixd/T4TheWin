using _CodeGenerator.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class ClassificationDefinitionsTemplate
    {
        public ClassificationDefinitionClassInfoProvider ClassInfo { get; }

        public ClassificationDefinitionsTemplate(ClassificationDefinitionClassInfoProvider classInfo)
        {
            this.ClassInfo = classInfo;
        }
    }
}
