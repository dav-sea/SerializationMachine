using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class ByteResolver : IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Byte.Parse(serialized.Value);
        }
        public ByteResolver() : base(TypeOf<byte>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (byte)0;
        }
    }
}
