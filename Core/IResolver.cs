using System.Xml.Linq;

namespace SerializeMachine.Core
{
    public interface IResolver
    {
        /// <summary>
        /// Сериализирует объект resolveObject в xml-узел serialized
        /// </summary>
        /// <param name="serialized">xml-узел в который следуют проводить сериализацию</param>
        /// <param name="resolveObject">Сериализируемый объект</param>
        void Serialize(XElement serialized, object resolveObject);
        /// <summary>
        /// Десериализирует serializedObject в объект 
        /// </summary>
        /// <param name="serialized">Сериализированная версия объекта</param>
        /// <returns>Дессериализированный объект</returns>
        object Deserialzie(XElement serializedObject);
    }
}
