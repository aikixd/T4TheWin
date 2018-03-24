using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Bridge
{
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MemberAttribute : Attribute
    {
        public MemberAttribute(MemberDiscovery memberDicovery)
        {

        }
    }
}
