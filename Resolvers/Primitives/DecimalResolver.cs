using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class DecimalResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Decimal.Parse(serialized.Value);
        }
    }
}
