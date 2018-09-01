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
    public sealed class RuntimeResolver : Resolver
    {
        private readonly IList<FieldInfo> Fields;

        public override sealed void Serialize(XElement serialized, object obj)
        {
            var leng = Fields.Count;
            for (int i = 0; i < leng; i++)
            {
                serialized.Add(Serializator.AutoResolve(Fields[i].GetValue(obj)));
            }
        }
        public override sealed void Deserialzie(XElement serialized,ref object instance)
        {
            var reflectionEnumerator = Fields.GetEnumerator();
            var serializedEnumerator = serialized.Elements().GetEnumerator();

            if(instance != null)
                while (reflectionEnumerator.MoveNext() && serializedEnumerator.MoveNext())
                    reflectionEnumerator.Current.SetValue(instance, Serializator.AutoDeresolve(serializedEnumerator.Current));
        }

        public RuntimeResolver(Type resolveType, Serializator serializator)
            : this(serializator, resolveType, true) { }

        public RuntimeResolver(Serializator serializator, Type resolveType,bool useConstructor)
            :base(resolveType,useConstructor,serializator)
        {
            if (resolveType != null)
            {
                Fields = SerializationUtility.Targeting.GetSerializableFieldsInternal(resolveType);
            }
            else
            {
                Fields = new FieldInfo[0];
            }
        }

        private RuntimeResolver(Type resolveType, Serializator serializator,IFactory factory)
            :base(factory,resolveType,serializator)
        {
            if (resolveType != null)
            {
                Fields = SerializationUtility.Targeting.GetSerializableFieldsInternal(resolveType);
            }
            else
            {
                Fields = new FieldInfo[0];
            }
        }

        public static RuntimeResolver ConfigurateRuntimeResolver(Type resolveType, Serializator serializator)
        {
            var defaultConstructor = SerializationUtility.Reflection.GetDefaultConstructor(resolveType);
            if (defaultConstructor != null)
                return new RuntimeResolver(resolveType, serializator, FactoryUtility.CreateConstructorFactory(defaultConstructor));

            return new RuntimeResolver(serializator, resolveType, false);
        }
    }
}
