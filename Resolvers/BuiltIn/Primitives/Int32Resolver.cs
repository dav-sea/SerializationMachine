using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class Int32Resolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int32.Parse(serialized.Value);
        }
        public Int32Resolver() : base(Utility.TypeOf<int>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return 0;
        }
    }
}
