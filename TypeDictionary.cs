using System;
using System.Collections.Generic;
using System.Xml.Linq;

using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class TypeDictionary
    {
        public const string XML_ELEMENTNAME_TYPEDICTIONARY = "DICTIONARY";

        private readonly SortedList<string, Type> TypeList;

        public int Count
        {
            get
            {
                return TypeList.Count;
            }
        }
        public int Capacity
        {
            get
            {
                return TypeList.Capacity;
            }
        }

        public string ConventionOf(Type type)
        {
            var index = TypeList.IndexOfValue(type);
            return index < 0 ? Serializator.GUID_NULL_TOSTRING : TypeList.Keys[index];
        }
        public Type TypeOf(string convention)
        {
            var index = TypeList.IndexOfKey(convention);
            return index < 0 ? null : TypeList.Values[index];
        }
        public bool TryGetConvention(Type type, out string convention)
        {
            var index = TypeList.IndexOfValue(type);
            if (index < 0)
            {
                convention = Serializator.GUID_NULL_TOSTRING;
                return false;
            }
            convention = TypeList.Keys[index];
            return true;
        }
        public bool TryGetType(string convention, out Type type)
        {
            return TypeList.TryGetValue(convention, out type);
        }

        public bool ContainsConvention(string convention)
        {
            return TypeList.ContainsKey(convention);
        }

        public void OverloadConvention(Type type, string convention)
        {
            DeleteAllConventionOf(type);
            AddConvention(type, convention);
        }
        public void AddConvention(Type type, string convention)
        {
            TypeList.Add(convention, type);
        }
        public void DeleteConvention(string convention)
        {
            TypeList.Remove(convention);
        }
        public void DeleteAllConventionOf(Type type)
        {
            int index = TypeList.IndexOfValue(type);
            while (index >= 0)
            {
                TypeList.RemoveAt(index);
                index = TypeList.IndexOfValue(type);
            }
        }

        public TypeDictionary(int dictionaryCapacity)
        {
            TypeList = new SortedList<string, Type>(dictionaryCapacity);
        }
        internal TypeDictionary(XElement serialized)
        {
            TypeList = new SortedList<string, Type>(50);//TODO CAPCACITY
            //if (serialized == null) throw new ArgumentNullException("serialized");//TODO REMOVE? (internal)
            LoadTypesInternal(serialized, TypeList);
        }

        public void OverloadTypes(XElement serialized)
        {
            if (serialized == null || serialized.Name != XML_ELEMENTNAME_TYPEDICTIONARY) return;
            
            TypeList.Clear();

            LoadTypesInternal(serialized, TypeList);
        }

        public void Clear()
        {
            TypeList.Clear();
        }

        public IDictionary<string,Type> ToDictionary()
        {
            return TypeList;
        }
        
        public static XElement CreateSerializedTypeDictionary(TypeDictionary typeDictionary)
        {
            if (typeDictionary == null) throw new ArgumentNullException("typeDictionary");
            var target = typeDictionary.ToDictionary();
            if(target == null) throw new InvalidOperationException("typeDictionary method ToDictionary() return null");
            return CreateSerializedTypeDictionary(target);
        }
        internal static XElement CreateSerializedTypeDictionary(IDictionary<string, Type> typeDictionary)
        {
            var serialized = new XElement(XML_ELEMENTNAME_TYPEDICTIONARY);

            foreach(var pair in typeDictionary)
                serialized.Add(
                    new XElement(
                        pair.Key,pair.Value.AssemblyQualifiedName.ToString()
                        )
                );

            return serialized;
        }
        internal static void LoadTypesInternal(XElement serialized,IDictionary<string,Type> here)
        {
            foreach (var element in serialized.Elements())
                here.Add(element.Name.LocalName, Type.GetType(element.Value));
        }
        public static TypeDictionary CreateNewTypeDictionary(XElement serialized)
        {
            return serialized == null ? new TypeDictionary(0) : new TypeDictionary(serialized);
        }
    }


}
//TODO MB
/*
 * internal static class TypeOf<T> 
 * {
 *     public static readonly Type Runtime = typeof(T);
 * }
 */