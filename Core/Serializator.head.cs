using System;
using SerializeMachine.Managers;

namespace SerializeMachine.Core
{
    public partial class Serializator
    {
        /// <summary>
        /// NULL-Метка для сериализатора. Узел имеющий данное имя 
        /// будет воспиниматься сериализатором как значение null
        /// </summary>
        internal const string XML_ELEMENTNAME_NULL = "NULL";
        /// <summary>
        /// Имя атрибута, в котором хранится GUID объекта
        /// </summary>
        internal const string XML_ATTRIBUTENAME_GUID = "GUID";
        /// <summary>
        /// Нулевая ссылка / ссылка на null
        /// </summary>
        internal static readonly Guid GUID_NULL;
        /// <summary>
        /// Предстваление GUID_NULL в строковом формате
        /// </summary>
        internal static readonly string GUID_NULL_TOSTRING;

        /// <summary>
        /// Менеджер куч
        /// </summary>
        private HeapManager HeapManager;
        /// <summary>
        /// Менеджер определения типов
        /// </summary>
        private TypeManager TypeManager;
        /// <summary>
        /// Менеджер resolver`ов
        /// </summary>
        private ResolverBank ResolversManager;

        public TypeManager GetTypeManager()
        {
            return TypeManager;
        }
        public HeapManager GetHeapManager()
        {
            return HeapManager;
        }
        public ResolverBank GetResolverBank()
        {
            return ResolversManager;
        }

        public Serializator()
            :this(new TypeManager(),new HeapManager())
        {
            TypeManager = new TypeManager();
            ResolversManager = new ResolverBank(this);
            HeapManager = new HeapManager();
        }

        public Serializator(TypeManager typeManager,HeapManager heapManager)
        {
            TypeManager = typeManager;
            HeapManager = heapManager;
            ResolversManager = new ResolverBank(this);
        }

        static Serializator()
        {
            GUID_NULL = Guid.Empty;
            GUID_NULL_TOSTRING = GUID_NULL.ToString();
        }

    }
}
