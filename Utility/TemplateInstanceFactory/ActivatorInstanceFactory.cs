using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public sealed class ActivatorInstanceFactory : ITemplateInstanceFactory
    {
        public readonly Type InstanceType;
        public object Instantiate(XElement arg)
        {
            return Activator.CreateInstance(InstanceType);
        }


        public ActivatorInstanceFactory(Type instanceType)
        {
            InstanceType = instanceType ?? throw new ArgumentNullException();
        }
    }
}
