using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class UInt16Resolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return System.UInt16.Parse(serialized.Value);
        }
    }
}
