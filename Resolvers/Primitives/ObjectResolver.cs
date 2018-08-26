using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ObjectResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = new object();
        }
    }
}
