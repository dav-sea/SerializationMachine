using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class UInt16Resolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.UInt16.Parse(serialized.Value);
        }
        public UInt16Resolver() : base(Utility.TypeOf<ushort>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (ushort)0;
        }
    }
}
