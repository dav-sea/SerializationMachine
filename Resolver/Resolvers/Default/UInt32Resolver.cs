using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class UInt32Resolver : IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.UInt32.Parse(serialized.Value);
        }
        public UInt32Resolver() : base(TypeOf<uint>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (uint)0;
        }
    }
}
