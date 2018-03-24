using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Domain
{
    public class Attribute
    {
        public Type Type { get; }
        public IDictionary<Argument, object> Arguments { get; }

        public Attribute(Type type, IDictionary<Argument, object> arguments)
        {
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }
    }
}
