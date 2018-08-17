using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SerializeMachine
{
    public sealed class TypeDictionary
    {
        public static readonly Guid GUID_NULL = Guid.Empty;
        public static readonly string CONVENTION_NULL = GUID_NULL.ToString();

        SortedList<string, Type> TypeList;

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

        public static XElement GetHead(string convention)
        {
            return new XElement(convention);
        }
    }


}
