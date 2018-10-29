using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public sealed class FuncInstanceFactory : ITemplateInstanceFactory
    {
        public readonly Func<XElement, object> FactoryFunc;
        public object Instantiate(XElement arg)
        {
            return FactoryFunc(arg);
        }
        public FuncInstanceFactory(Func<XElement, object> factoryFunc)
        {
            FactoryFunc = factoryFunc ?? throw new ArgumentNullException();
        }
    }
}
