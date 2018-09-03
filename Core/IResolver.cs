using System;
using System.Xml.Linq;

namespace SerializeMachine.Core
{
    /// <summary>
    /// Базовывй класс для всех Resolver`ов
    /// </summary>
    public abstract class IResolver
    {
        /// <summary>
        /// Тип с которым работает данный IResolver
        /// </summary>
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

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="resolveType"></param>
        internal IResolver(Type resolveType)
        {
            this.ResolveType = resolveType;
        }

        /// <summary>
        /// Метод создающий новый экхемпляр типа ResolveType. Данный экземпляр будет использован для
        /// десериализации объекта путем передачи данного экземпляра в метод Deserialzie.
        /// Данный метод вызывается напрямую из Serializator для регестрации экземпляра по GUID.
        /// 
        /// Note: Благадаря схеме: инстаницирование -> сохранение ссылки -> десериализация объекта 
        /// Осуществеленна возможность сохранения само-циклических и перекресных ссылок  
        /// </summary>
        /// <param name="serializedObject">Сериализированная версия объекта</param>
        /// <returns>Новывй экземпляр типа ResolveType</returns>
        internal protected abstract object ManagedObjectOf(XElement serializedObject);

        public override bool Equals(object obj)
        {
            if (obj == this) return true;

            var otherResolver = obj as IResolver;
            if (otherResolver == null) return false;

            return otherResolver.ResolveType == ResolveType;

        }
        public override int GetHashCode()
        {
            return ResolveType.GetHashCode() * 7;
        }
        public override string ToString()
        {
            return string.Format("{{0}, ResolveType - {1}}", GetType().Name, ResolveType.FullName);
        }
    }
}
