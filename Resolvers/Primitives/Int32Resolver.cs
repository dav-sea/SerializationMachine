using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class Int32Resolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return System.Int32.Parse(serialized.Value);
        }
    }
}
