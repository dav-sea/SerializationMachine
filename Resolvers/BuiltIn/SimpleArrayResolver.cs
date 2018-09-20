using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerializationMachine.Core;
using System.Xml.Linq;
using SerializationMachine.Utility;
using System.Reflection;

namespace SerializationMachine.Resolvers.BuiltIn
{
    public sealed class SimpleArrayResolver : IResolver
    {
        private readonly Type ElementType;
        private readonly Serializator Serializator;

        public override void Serialize(XElement serialized, object resolveObject)
        {
            var array = resolveObject as Array;
            var leng = array.GetLength(0);

            serialized.SetAttributeValue("SIZE", leng);

            for (int i = 0; i < leng; i++)
                serialized.Add(Serializator.AutoResolve(array.GetValue(i)));
        }

        public override void Deserialzie(XElement serializedObject, ref object instance)
        {
            var array = instance as Array;
            var leng = array.GetLength(0);

            int currentIndex = 0;
            var serializedEnumerator = serializedObject.Elements().GetEnumerator();

            while (serializedEnumerator.MoveNext())
            {
                array.SetValue(Serializator.AutoDeresolve(serializedEnumerator.Current), currentIndex++);
            }

        }

        public SimpleArrayResolver(Type resolveType, Serializator serializator)
            : base(resolveType)
        {
            if (!resolveType.IsArray) throw new ArgumentException();
            this.Serializator = serializator;
            ElementType = resolveType.GetElementType(); ;
        }


        protected internal override sealed object ManagedObjectOf(XElement serializedObject)
        {
            var sizeAttribute = serializedObject.Attribute("SIZE");
            var leng = int.Parse(sizeAttribute.Value);
            return Array.CreateInstance(ElementType, leng);
        }
    }
}
