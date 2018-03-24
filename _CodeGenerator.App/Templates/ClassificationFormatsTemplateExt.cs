using _CodeGenerator.Definitions;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class ClassificationFormatsTemplate
    {
        public string Namespace { get; }
        public TemplateFormatClassInfoProvider[] ClassInfos { get; }

        public ClassificationFormatsTemplate(string @namespace, TemplateFormatClassInfoProvider[] classInfos)
        {
            this.Namespace = @namespace;
            this.ClassInfos = classInfos;
        }
    }
}
