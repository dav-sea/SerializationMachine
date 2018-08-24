using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class DoubleResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return System.Double.Parse(serialized.Value);
        }
    }
}
