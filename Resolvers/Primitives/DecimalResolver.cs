using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class DecimalResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public object Deserialzie(XElement serialized)
        {
            return System.Decimal.Parse(serialized.Value);
        }
    }
}
