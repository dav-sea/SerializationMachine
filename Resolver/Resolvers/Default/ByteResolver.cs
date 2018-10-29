using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
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
        public ByteResolver() : base(Utility.TypeOf<byte>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (byte)0;
        }
    }
}
