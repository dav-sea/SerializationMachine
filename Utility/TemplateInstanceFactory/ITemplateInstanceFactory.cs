using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public interface ITemplateInstanceFactory
    {
        object Instantiate(XElement arg);
    }
}
