using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    /// <summary>
    /// Стандартный сериализатор типа Int16 (short)
    /// </summary>
    public sealed class Int16Resolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Int16.Parse(serialized.Value);
        }
        public Int16Resolver() : base(Utility.TypeOf<short>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return (short)0;
        }
    }
}
