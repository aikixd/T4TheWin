using System;

namespace _CodeGenerator.Bridge
{
    /// <summary>
    /// Marks the class to include it for IEquality code generation.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GenerateEqualityAttribute : Attribute
    {
        public GenerateEqualityAttribute()
        {
            
        }

        public GenerateEqualityAttribute(MemberDiscovery memberDicovery)
        {

        }
    }
}
