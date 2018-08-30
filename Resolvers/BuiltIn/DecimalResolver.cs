using System.Xml.Linq;

namespace SerializeMachine.Resolvers
{
    public sealed class DecimalResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Decimal.Parse(serialized.Value);
        }
        public DecimalResolver() : base(Utility.TypeOf<decimal>.Type) { }
        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return 0.0M;
        }
    }
}
