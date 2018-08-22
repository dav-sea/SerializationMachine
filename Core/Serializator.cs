using System;
using System.Collections.Generic;
using System.Xml.Linq;

using SerializeMachine.Utility;

namespace SerializeMachine.Core
{
    public sealed class Serializator
    {
        internal const string XML_ATTRIBUTENAME_GUID = "GUID";
        internal static readonly Guid GUID_NULL = Guid.Empty;

        public HeapManager HeapManager;
        public TypeManager TypeManager;
        public ResolverBank ResolverBank;

        public void FlashHeap()
        {
            HeapManager.Serialized.ClearSerialized();
        }

        public object Deresolve(XElement serialized)
        {
            var convention = TypeManager.TypeOf(serialized.Name.LocalName);
            //ResolverBank.GetResolver(convention
        }

        internal object DeresolveInternal(XElement serializedObject, IResolver resolver)
        {
            return resolver.Deserialzie(serializedObject);
        }

        /// <summary>
        /// Сериализирует объект resolveObject в возвращаемый xml-узел, используя действительный сериализатор для типа объекта resolveObject
        /// Допускается значение null для resolveObject, в данном случае будет возвращен пустой xml-узел с именем соответствующему имени нулевого объекта  
        /// Метод является контекстным. 
        /// Обрашается к менеджеру типов для определения условного обозноечения типа.
        /// </summary>
        /// <param name="resolveObject">Сериализируемый объект</param>
        /// <returns>xml-узел являющейся результатом сериализации</returns>
        public XElement Resolve(object resolveObject)
        {
            if (resolveObject == null)
                return TypeDictionary.GetNullHead();
            else
            {
                var conventionType = TypeManager.ConventionOf(resolveObject.GetType());
                var serialized = TypeDictionary.GetHead(conventionType);
                ResolveToInternal(resolveObject, conventionType, serialized);
                return serialized;
            }
        }
        
        /// <summary>
        /// Сериализирует объект resolveObject в xml-узел serializeHere используя действительный сериализатор для объектов типа conventionType
        /// Не допускается значение null ни для какаих входных параметров.
        /// Метод является контекстным. 
        /// Обращается к банку resolver`ов для получения resolver`а 
        /// </summary>
        /// <param name="resolveObject">Сериализируемый объект</param>
        /// <param name="conventionType">Условное обозночение типа</param>
        /// <param name="serializeHere">xml-узел в который будет записан результат сериализации</param>
        internal void ResolveToInternal(object resolveObject, string conventionType, XElement serializeHere)
        {
            ResolverBank.GetResolver(conventionType).Serialize(serializeHere, resolveObject);
        }

        /// <summary>
        /// Сериализирует объект resolveObject в xml-узел serializeHere используя указанный resolver.
        /// Не допускается значение null ни для какаих входных параметров.
        /// resolver должен соответствовать типу сериализирумого объекта (resolveObject).
        /// Метод является внеконтекстным, однако указаный в нем resolver может обращаться к контексту сериализатора (куче,словарю типов и тд)
        /// </summary>
        /// <param name="resolveObject">Сериализируемый объект</param>
        /// <param name="resoler">Resolver осуществляющий сериализацию</param>
        /// <param name="serializeHere">xml-узел в который будет записан результат сериализации</param>
        internal void ResolveToInternal(object resolveObject, IResolver resoler, XElement serializeHere)
        {
            resoler.Serialize(serializeHere, resolveObject);
        }
        
        public XElement GetFieldSerialized(object obj)
        {
            var type = obj.GetType();
            var convention = TypeManager.ConventionOf(type);
            var result = TypeDictionary.GetHead(convention);

            if (SerializationUtility.Targeting.IsSaveReferenceInternal(type))
            {
                Guid guid;
                if (HeapManager.GetCreateGuid(obj, out guid))
                    HeapManager.Serialized.Push(guid, Resolve(obj));
                result.Value = guid.ToString();
            }
            else
            {
                ResolveToInternal(obj, convention, result);
            }

            return result;
        }

        public Serializator()
        {
            TypeManager = new global::SerializeMachine.TypeManager();
            ResolverBank = new global::SerializeMachine.ResolverBank(this);
            HeapManager = new HeapManager(this);
        }
    }
}
