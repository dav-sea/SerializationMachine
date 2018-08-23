using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

using System.Runtime.Serialization;

using SerializeMachine.Core;
using SerializeMachine.Utility;

namespace SerializeMachine.Resolvers
{
    /// <summary>
    /// Resolver 
    /// </summary>
    public sealed class RuntimeResolver : IResolver
    {
        private readonly Type ResolveType;
        private readonly Serializator Serializator;
        private readonly IList<FieldInfo> Fields;

        public void Serialize(XElement serialized, object obj)
        {
            var leng = Fields.Count;
            for (int i = 0; i < leng; i++)
            {
                serialized.Add(Serializator.ContextResolve(Fields[i].GetValue(obj)));
            }
        }

        public object Deserialzie(XElement serialized)
        {
            var reflectionEnumerator = Fields.GetEnumerator();
            var serializedEnumerator = serialized.Elements().GetEnumerator();

            object instance = CreateNewInstance();

            while (reflectionEnumerator.MoveNext() && serializedEnumerator.MoveNext())
            {
                reflectionEnumerator.Current.SetValue(instance, Serializator.ContextDeresolve(serializedEnumerator.Current));
            }

            return instance;
        }

        private object CreateNewInstance()
        {
            return FormatterServices.GetUninitializedObject(ResolveType);
        }

        public RuntimeResolver(Type resolveType,Serializator serializator)
        {
            if (resolveType != null)
            {
                this.ResolveType = resolveType;
                this.Serializator = serializator;
                Fields = SerializationUtility.Targeting.GetSerializableFieldsInternal(resolveType).ToArray();
            }
            else Fields = new FieldInfo[0];
        }

       /* private struct FieldResolver : IResolver
        {
            private RuntimeResolver RuntimeResolver;
            private Action<XElement, object, RuntimeResolver> SerializeMethod;
            private Func<object, XElement> DeserializeMethod;

            public void Serialize(XElement serialized, object obj)
            {
                SerializeMethod(serialized, obj, RuntimeResolver);
            }
            public object Deserialzie(XElement serialized)
            {
                return DeserializeMethod(serialized);
            }

            public static FieldResolver BuildResolver(bool referenceSave, bool safelySerialization, bool safelyDeserialization)
            {

            }
            private static Action<XElement, object, RuntimeResolver> BuildSerializeMethod(bool referenceSave, bool safely)
            {
                if (referenceSave)
                {
                    if (safely)
                    {
                        return delegate(XElement serialized,object obj,RuntimeResolver resolver)
                        {
                            Guid guid;
                            if (serialized == null) return;
                            if (obj == null || obj.GetType() != resolver.ResolveType)
                            {
                                serialized.Value = "";
                                serialized.Add(new XAttribute("GUID",TypeDictionary.CONVENTION_NULL));
                            }
                            resolver.Serializator.GetSerialized(obj,out guid);
                            serialized.Value = guid.ToString();
                        };
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (safely)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
        */
    }
}
