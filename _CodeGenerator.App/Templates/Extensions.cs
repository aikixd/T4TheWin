using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    static class Extensions
    {
        public static bool IsSameType(this Domain.Type type, Type otherType)
        {
            return
                type.Name == otherType.Name &&
                type.Namespace == otherType.Namespace;
        }
    }
}
