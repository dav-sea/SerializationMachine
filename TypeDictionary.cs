using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerializeMachine
{
    public sealed class TypeDictionary
    {
        public const string XML_ELEMENTNAME_TYPEDICTIONARY = "DICTIONARY";
        public static readonly string CONVENTION_NULL = "NULL";

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
            return index < 0 ? CONVENTION_NULL : TypeList.Keys[index];
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
                convention = CONVENTION_NULL;
                return false;
            }
            convention = TypeList.Keys[index];
            return true;
        }
        public bool TryGetType(string convention, out Type type)
        {
            return TypeList.TryGetValue(convention, out type);
        }

        public void AddConvention(Type type, string convention)
        {
            TypeList.Add(convention, type);
        }
        public void DeleteConvention(string convention)
        {
            TypeList.Remove(convention);
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

        public static XElement GetHead(string convention)
        {
            return new XElement(convention);
        }
        public static void SetNullSerialized(XElement element)
        {
            element.Value = null;
            element.Name = CONVENTION_NULL;
        }
        public static XElement GetNullHead()
        {
            return new XElement(CONVENTION_NULL);
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