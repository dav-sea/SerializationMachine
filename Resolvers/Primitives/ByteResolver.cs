using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ByteResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return System.Byte.Parse(serialized.Value);
        }
    }
}
