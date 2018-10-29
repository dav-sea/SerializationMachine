using System.Xml.Linq;
using SerializationMachine.Utility;

namespace SerializationMachine.Resolver.Resolvers
{
    public class EmptyResolver : Resolver
    {
        public EmptyResolver(Serializator serializator) 
            : base(new NullTemplateFactory(), TypeOf<object>.Type, serializator)
        {
        }

        public override void Deserialzie(XElement serializedObject, ref object instance)
        {

        }

        public override void Serialize(XElement serialized, object resolveObject)
        {

        }
    }
}
