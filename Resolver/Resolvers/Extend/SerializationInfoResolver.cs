using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;

using SerializationMachine.Utility;


namespace SerializationMachine.Resolvers
{
    public sealed class SerializationInfoResolver : Resolver
    {
        private const string XML_ELEMENTNAME_ENTRYSCONTAINER = "ENTRYS";
        private const string XML_ATTRIBUTENAME_NAME = "NAME";

        public override void Serialize(System.Xml.Linq.XElement serialized, object resolveObject)
        {
            var serializationInfo = resolveObject as SerializationInfo;

            var entryEnumerator = serializationInfo.GetEnumerator();

            var entryContainer = new XElement(XML_ELEMENTNAME_ENTRYSCONTAINER);

            XElement current;

            while (entryEnumerator.MoveNext())
            {
                current = Serializator.AutoResolve(entryEnumerator.Current.Value);
                current.Add(new XAttribute(XML_ATTRIBUTENAME_NAME, entryEnumerator.Current.Name));
                entryContainer.Add(current);
            }

            serialized.Add(entryContainer);
        }

        public override void Deserialzie(System.Xml.Linq.XElement serializedObject, ref object instance)
        {
            var serializationInfo  = instance as SerializationInfo;

            var entryContainer = serializedObject.Element(XML_ELEMENTNAME_ENTRYSCONTAINER);

            var entryEnumerator = entryContainer.Elements().GetEnumerator();

            object current;

            while (entryEnumerator.MoveNext())
            {
                current = Serializator.AutoDeresolve(entryEnumerator.Current);
                serializationInfo.AddValue(entryEnumerator.Current.Attribute(XML_ATTRIBUTENAME_NAME).Value, current);
            }
        }

        public SerializationInfoResolver(Core.Serializator serializator)
            : base(typeof(SerializationInfo), SerializationInfoFactory, serializator)
        {
            
        }

        private static object SerializationInfoFactory(XElement serialized)
        {
            return new SerializationInfo(TypeOf<Object>.Type, new FormatterConverter());
        }
    }
}
