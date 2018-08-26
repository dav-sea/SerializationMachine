using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class CharResolver : Core.IResolver
    {
        public void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Char.Parse(serialized.Value);
        }
    }
}
