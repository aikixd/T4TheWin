using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _CodeGenerator.Definitions
{
    public class CustomClassInfoProvider : IClassInfoProvider
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public Accessabilty Accessability { get; set; }

        public bool IsStatic { get; set; }

        public bool IsSealed { get; set; }

        public IEnumerable<ClassMember> Members { get; set; }

        public IEnumerable<Domain.Attribute> Attributes { get; set; }

        public CustomClassInfoProvider(
            string name,
            string @namespace)
        {
            this.Name      = name;
            this.Namespace = @namespace;

            this.Members    = Enumerable.Empty<ClassMember>();
            this.Attributes = Enumerable.Empty<Domain.Attribute>();
        }
    }
}
