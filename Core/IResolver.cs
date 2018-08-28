using System;
using System.Xml.Linq;
using SerializeMachine.Utility;

namespace SerializeMachine.Core
{
    public abstract class IResolver
    {
        protected readonly Type ResolveType;
        private readonly IFactory InstanceFactory;
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

        public IResolver(IFactory instanceFactory, Type resolveType)
        {
            this.ResolveType = resolveType;
            if (instanceFactory == null) this.InstanceFactory = new NullFactory();
            else this.InstanceFactory = instanceFactory;
        }
        public IResolver(Type resolveType ,bool useConstructor)
        {
            this.ResolveType = resolveType;
            if (useConstructor) this.InstanceFactory = Factory.CreateConstructorFactory(resolveType);
            else this.InstanceFactory = Factory.CreateUninitializedFactory(resolveType);
        }
        public IResolver(Type resolveType, Func<object> customFactory)
        {
            this.ResolveType = resolveType;
            if (customFactory == null) this.InstanceFactory = new NullFactory();
            else this.InstanceFactory = Factory.CreateCustomFactory(customFactory);
        }

        internal object GetNewInstance()
        {
            return InstanceFactory.Instantiate();
        }
    }
}
