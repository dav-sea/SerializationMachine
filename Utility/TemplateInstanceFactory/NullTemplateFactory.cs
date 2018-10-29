using System;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public sealed class NullTemplateFactory : ITemplateInstanceFactory
    {
        public object Instantiate(XElement arg)
        {
            return null;
        }
    }
}
