using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public sealed class DefaultTemplateFactory : ITemplateInstanceFactory
    {
        ITemplateInstanceFactory Factory;
        public object Instantiate(XElement arg)
        {
            return Factory.Instantiate(arg);
        }

        public DefaultTemplateFactory(Type templateType)
        {
            if (templateType.IsValueType)
                Factory = new ActivatorInstanceFactory(templateType);
            else Factory = new NullTemplateFactory();
        }
    }
}
