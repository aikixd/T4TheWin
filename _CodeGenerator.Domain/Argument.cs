using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Domain
{
    public class Argument : IEquatable<Argument>
    {
        public string Name { get; }
        public Type Type { get; }

        public Argument(string name, Type type)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public bool Equals(Argument other)
        {
            return this.Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Argument x)
                return this.Equals(x);
            return false;
        }

        public static bool operator == (Argument left, Argument right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Argument left, Argument right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
