using System.Xml.Linq;

namespace SerializationMachine.Resolvers.Primitives
{
    public sealed class BooleanResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.Boolean.Parse(serialized.Value);
        }
        public BooleanResolver() : base(Utility.TypeOf<bool>.Type) { }
        protected internal override object GetTemplateInstance(XElement serializedObject)
        {
            return false;
        }
    }
}
