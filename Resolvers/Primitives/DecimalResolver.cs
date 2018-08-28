using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
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
        public DecimalResolver() : base(Utility.TypeOf<decimal>.Type, null) { }
    }
}
