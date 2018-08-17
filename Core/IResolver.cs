using System.Xml.Linq;

namespace SerializeMachine.Core
{
    public interface IResolver
    {
        void Serialize(XElement serialized, object obj);
        object Deserialzie(XElement serialized);
    }
}
