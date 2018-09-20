using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class UInt32Resolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.UInt32.Parse(serialized.Value);
        }
        public UInt32Resolver() : base(Utility.TypeOf<uint>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return (uint)0;
        }
    }
}
