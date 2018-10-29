using System;
using System.Xml.Linq;
using System.Reflection;

namespace SerializationMachine.Utility
{
    public sealed class ConstructorInstanceFactory : ITemplateInstanceFactory
    {
        public readonly ConstructorInfo Constructor;

        public object Instantiate(XElement arg)
        {
            return Constructor.Invoke(null);
        }
        public ConstructorInstanceFactory(Type instanceType)
        {
            if (instanceType == null) throw new ArgumentNullException();
            Constructor = SerializationUtility.Reflection.GetDefaultConstructor(instanceType);
        }
    }
}
