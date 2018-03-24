using System;
using System.Collections.Generic;
using System.Text;

namespace _CodeGenerator.Bridge
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GenerateDisposableAttribute : Attribute
    {
        public GenerateDisposableAttribute() { }
        public GenerateDisposableAttribute(DisposableOption option) { }
    }
}
