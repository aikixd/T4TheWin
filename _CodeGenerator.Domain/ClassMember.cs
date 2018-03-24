using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.Domain
{
    public class ClassMember
    {
        public string Name { get; }
        public Type Type { get; }
        public IEnumerable<Attribute> Attributes { get; }

        public ClassMember(
            string name,
            Type type)
            : this(name, type, Enumerable.Empty<Attribute>()) { }

        public ClassMember(
            string name,
            Type type,
            IEnumerable<Attribute> attrs)
        {
            this.Name = name;
            this.Type = type;
            this.Attributes = attrs;
        }
    }
}
