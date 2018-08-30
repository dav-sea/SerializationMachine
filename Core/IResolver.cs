using System;
using System.Xml.Linq;

namespace SerializeMachine.Core
{
    public abstract class IResolver
    {
        protected readonly Type ResolveType;
        /// <summary>
        /// Сериализирует объект resolveObject в xml-узел serialized
        /// </summary>
        /// <param name="serialized">xml-узел в который следуют проводить сериализацию</param>
        /// <param name="resolveObject">Сериализируемый объект</param>
        public abstract void Serialize(XElement serialized, object resolveObject);
        /// <summary>
        /// Десериализирует serializedObject в объект 
        /// </summary>
        /// <param name="serialized">Сериализированная версия объекта</param>
        /// <returns>Дессериализированный объект</returns>
        public abstract void Deserialzie(XElement serializedObject, ref object instance);

        internal IResolver(Type resolveType)
        {
            this.ResolveType = resolveType;
        }

        internal protected abstract object ManagedObjectOf(XElement serializedObject);
    }
}
