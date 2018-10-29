using System;
using System.Xml.Linq;
using SerializationMachine.Core;
using SerializationMachine.Utility;

namespace SerializationMachine.Resolvers
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
