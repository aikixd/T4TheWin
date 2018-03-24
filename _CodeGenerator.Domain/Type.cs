using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Domain
{
    public class Type : IEquatable<Type>
    {
        public string Name { get; }
        public string Namespace { get; }

        public Type(
            string name,
            string @namespace)
        {
            this.Name = name;
            this.Namespace = @namespace;
        }

        public bool Equals(Type other)
        {
            return
                this.Name == other.Name &&
                this.Namespace == other.Namespace;
        }

        public override bool Equals(object obj)
        {
            if (obj is Type t)
                return this.Equals(t);
            return false;
        }

        public override int GetHashCode()
        {
            int i = 0;

            unchecked
            {
                i += this.Name.GetHashCode();
                i += this.Namespace.GetHashCode();
            }

            return i;
        }
    }
}
