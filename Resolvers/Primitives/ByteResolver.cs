using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ByteResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Byte.Parse(serialized.Value);
        }
    }
}
