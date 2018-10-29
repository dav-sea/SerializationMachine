using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class UInt16Resolver : IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.UInt16.Parse(serialized.Value);
        }
        public UInt16Resolver() : base(TypeOf<ushort>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (ushort)0;
        }
    }
}
