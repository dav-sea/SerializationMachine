using System.Xml.Linq;

namespace SerializationMachine.Resolver.Resolvers
{
    public sealed class Int32Resolver : IResolver
    {
        public override void Serialize(XElement serialized, object obj)
        {
            serialized.Value = obj.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int32.Parse(serialized.Value);
        }
        public Int32Resolver() : base(TypeOf<int>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return 0;
        }
    }
}
