using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    internal sealed class IntegerResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return int.Parse(serialized.Value);
        }
    }
}
