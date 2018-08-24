using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ObjectResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            
        }
        public object Deserialzie(XElement serialized)
        {
            return new object();
        }
    }
}
