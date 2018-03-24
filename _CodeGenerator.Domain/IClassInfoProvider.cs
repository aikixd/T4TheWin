using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.Domain
{
    public interface IClassInfoProvider
    {
        string Name { get; }
        string Namespace { get; }

        Accessabilty Accessability { get; }

        bool IsStatic { get; }

        bool IsSealed { get; }

        IEnumerable<ClassMember> Members { get; }

        IEnumerable<Attribute> Attributes { get; }
    }

    public enum Accessabilty
    {
        Inherited,
        Public,
        Private,
        Internal,
        Protected
    }
}
