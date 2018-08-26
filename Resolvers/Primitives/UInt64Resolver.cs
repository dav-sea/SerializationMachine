using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class UInt64Resolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.UInt64.Parse(serialized.Value);
        }
    }
}
