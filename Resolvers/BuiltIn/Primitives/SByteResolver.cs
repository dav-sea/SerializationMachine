using System.Xml.Linq;

namespace SerializeMachine.Resolvers.Primitives
{
    public sealed class SByteResolver : Core.IResolver
    {
        public override void Serialize(XElement serialized, object resolveObject)
        {
            serialized.Value = resolveObject.ToString();
        }
        public override void Deserialzie(XElement serialized,ref object instance)
        {
            instance = System.SByte.Parse(serialized.Value);
        }
        public SByteResolver() : base(Utility.TypeOf<sbyte>.Type) { }


        protected internal override object ManagedObjectOf(XElement serializedObject)
        {
            return (sbyte)0;
        }
    }
}
