using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class ByteResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Byte.Parse(serialized.Value);
        }
        public ByteResolver() : base(Utility.TypeOf<byte>.Type, null) { }
    }
}
