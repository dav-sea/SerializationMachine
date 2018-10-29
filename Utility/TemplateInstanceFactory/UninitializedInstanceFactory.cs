using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public sealed class UninitializedInstanceFactory : ITemplateInstanceFactory
    {
        public readonly Type InstanceType;
        public object Instantiate(XElement arg)
        {
            return SerializationUtility.InstantiateUninitializedObject(InstanceType);
        }

        public UninitializedInstanceFactory(Type instanceType)
        {
            InstanceType = instanceType ?? throw new ArgumentNullException();
        }
    }
}
