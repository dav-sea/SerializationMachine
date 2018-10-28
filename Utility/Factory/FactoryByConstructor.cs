using System;
using System.Reflection;

namespace SerializationMachine.Utility
{
    [Obsolete("Use TemplateInstanceFactory")]
    internal sealed class FactoryByConstructor : IFactory
    {
        private readonly ConstructorInfo Constructor;

        public object Instantiate()
        {
            return Constructor.Invoke(null);
        }

        internal FactoryByConstructor(ConstructorInfo constructor)
        {
            this.Constructor = constructor;
        }
    }
}
