using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SerializationMachine.Utility;

using SerializationMachine.Core;

namespace SerializationMachine
{
    public sealed class TypeDictionary
    {
        public const string XML_ELEMENTNAME_TYPEDICTIONARY = "DICTIONARY";

        private readonly SortedList<string, Type> TypeList;
        //private readonly SortedList<string, string> Modifications;

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
        public bool ContainsType(Type type)
        {
            return TypeList.ContainsValue(type);
        }

        /// <summary>
        /// Устанавливает convention для указанного типа. Если указанный convention
        /// уже определен то он будет перегружен новым
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="convention">Convention</param>
        public void SetConvention(Type type,string convention)
        {
            if (type == null) throw new ArgumentNullException();
            if (string.Empty.Equals(convention)) throw new ArgumentException();

            var index = TypeList.IndexOfKey(convention);
            if (index >= 0)
                TypeList.RemoveAt(index);

            TypeList.Add(convention, type);
        }
        /// <summary>
        /// Устанавливает convention для указанного типа. Если указанный convention
        /// уже определен то он будет перегружен новым. Нет проверки входных параметров.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="convention">Convention</param>
        public void SetConventionInternal(Type type, string convention)
        {
            var index = TypeList.IndexOfKey(convention);
            if (index >= 0)
                TypeList.RemoveAt(index);

            TypeList.Add(convention, type);
        }
        /// <summary>
        /// Устанавливает Convention для указанного типа. Нет проверки входных параметров.
        /// Нет проверки на колизию условных обозночений 
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="convention">Convention</param>
        internal void AddConventionInternal(Type type, string convention)
        {
            TypeList.Add(convention, type);
        }
        /// <summary>
        /// Удаляет указаный convention из словаря
        /// </summary>
        /// <param name="convention">Convention.</param>
        public void DeleteConvention(string convention)
        {
            TypeList.Remove(convention);
        }

        public void Reserve(int reserveCapacity)
        {
            TypeList.Capacity += reserveCapacity;
        }

        [Obsolete("Use SetConvention")]
        public void OverloadConvention(Type type, string convention)
        {
            DeleteAllConventionOf(type);
            AddConvention(type, convention);
        }
        [Obsolete("Use SetConvention")]
        public void AddConvention(Type type, string convention)
        {
            TypeList.Add(convention, type);
        }

        [Obsolete("Dont use this method")]
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
        [Obsolete("use static method LoadTypesInternal")]
        internal TypeDictionary(XElement serialized)
        {
            TypeList = new SortedList<string, Type>(50);//TODO CAPCACITY
            //if (serialized == null) throw new ArgumentNullException("serialized");//TODO REMOVE? (internal)
            LoadTypes(this, serialized);
        }

        [Obsolete("Use static method LoadTypes(TypeDictionary,XElement)")]
        public void OverloadTypes(XElement serialized)
        {
            if (serialized == null || serialized.Name != XML_ELEMENTNAME_TYPEDICTIONARY) return;
            
            TypeList.Clear();

            LoadTypes(this, serialized);
        }

        public void Clear()
        {
            TypeList.Clear();
        }

        [Obsolete("Dont use this method. TypeDictionary is IDictionary<string,Type>")]
        public IDictionary<string,Type> ToDictionary()
        {
            var count = Count;
            var result = new Dictionary<string, Type>(count);
            var dictionaryEnumerator = TypeList.GetEnumerator();

            while(dictionaryEnumerator.MoveNext())
                result.Add(dictionaryEnumerator.Current.Key, dictionaryEnumerator.Current.Value);

            return result;
        }

        public static XElement ToXML(TypeDictionary dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException();

            var node = new XElement(XML_ELEMENTNAME_TYPEDICTIONARY);
            var dictionaryEnumerator = dictionary.TypeList.GetEnumerator();

            while(dictionaryEnumerator.MoveNext())
            {
                node.Add(new XElement(dictionaryEnumerator.Current.Key, dictionaryEnumerator.Current.Value.AssemblyQualifiedName));
            }

            return node;
        }
        public static void LoadTypes(TypeDictionary dictionary, XElement dictionaryNode)
        {
            if (dictionaryNode == null || dictionary == null) return;
            if (!IsTypeDictionaryNode(dictionaryNode)) throw new FormatException();
            //TODO dictionary.Reserve(dictionaryNode.elemennscount)
            LoadTypesInternal(dictionary, dictionaryNode.Elements());
        }
        internal static void LoadTypesInternal(TypeDictionary dictionary, IEnumerable<XElement> typeNodes)
        {
            var nodeEnumerator = typeNodes.GetEnumerator();

            Type tempType;

            while(nodeEnumerator.MoveNext())
            {
                tempType = Type.GetType(nodeEnumerator.Current.Value);
                if(tempType == null)
                {
                    //TODO реагировать в зависимости от выбранных настроек загрузки словаря 
                    continue;
                }
                dictionary.SetConventionInternal(tempType, nodeEnumerator.Current.Name.LocalName);
            }
        }
        private static bool IsTypeDictionaryNode(XElement node)
        {
            return node.Name == XML_ELEMENTNAME_TYPEDICTIONARY;
        }
    }
}