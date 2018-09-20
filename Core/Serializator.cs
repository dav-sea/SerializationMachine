using System;
using System.Collections.Generic;
using System.Xml.Linq;

using SerializationMachine.Utility;
using SerializationMachine.Managers;

namespace SerializationMachine.Core
{
    public  sealed partial class Serializator
    {
        /// <summary>
        /// Отчищает все кучи. 
        /// Вызывает HeapManager.FlashHeaps();
        /// </summary>
        public void FlashHeaps()
        {
            HeapManager.FlashHeaps();
        }

        /// <summary>
        /// Сериализирует resolveObject в target.
        /// Данный метод формирует тело сериализированной версии объекта.
        /// 
        /// Important Note: Данный метод не взаимодействет с кучами, предпологается что все необходимые 
        /// манипуляции с кучами были сделаны до вызова этого метода 
        /// </summary>
        /// <param name="resolveObject">Объект который необходиом сериализировать</param>
        /// <param name="target">xml-узел в который необходимо произвести сериализацию</param>
        public void ResolveTo(object resolveObject, XElement target)
        {
            //Вызов внутреннего метода 
            ResolveToInternal(
                resolveObject,
                ResolversManager.GetResolver(TypeManager.ConventionOf(resolveObject.GetType())),
                target
            );
        }
        /// <summary>
        /// Десериализирует serializedObject в target.
        /// Данный метод формирует состоняеие экземпляра.
        /// 
        /// Important Note: Данный метод не взаимодействет с кучами, предпологается что все необходимые 
        /// манипуляции с кучами были сделаны до вызова этого метода 
        /// </summary>
        /// <param name="instance">Объект в который буде производиться десериализация</param>
        /// <param name="serializedObject"></param>
        public void DeresolveTo(object target, XElement serializedObject)
        {
            //Вызов внутреннего метода 
            DeresolveInternal(
                serializedObject,
                ref target,
                ResolversManager.GetResolver(TypeManager.ConventionOf(target.GetType()))
            );
        }

        /// <summary>
        /// Сериализирует объект, наиболее подходящим для него способом.
        /// Допускается значение null.
        /// </summary>
        /// <param name="resolveObject"></param>
        /// <returns></returns>
        public XElement AutoResolve(object resolveObject)
        {
            if (resolveObject == null)
                return XMLUtility.CreateNullNode();

            var type = resolveObject.GetType();//Запоминаем тип //TODO REMOVE THIS
            var conventionType = TypeManager.ConventionOf(type);//Получаем условное обозночение типа
            //Для начала пытаемся понять с чем мы вообще работаем 
            //TODO WORKING WITH TYPE MANAGER!
            if (SerializationUtility.Targeting.IsSaveReferenceInternal(type))
                return HeapResolve(resolveObject, conventionType);
            return ResolveInternal(resolveObject, conventionType);
        }
        /// <summary>
        /// Десериализирует объект, наиболее подходящим для него способом.
        /// Допускается значение соответствеющие* значению NULL.
        /// 
        /// *xml-узел должен иметь вид <NULL />. Значение NULL для самого узла serializedObject недопустимо
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public object AutoDeresolve(XElement serializedObject)
        {
            if (XMLUtility.IsNullOf(serializedObject))
                return null;

            var conventionType = serializedObject.Name.LocalName;
            var type = TypeManager.TypeOf(conventionType);
            var resolver = ResolversManager.GetResolver(conventionType);

            if (SerializationUtility.Targeting.IsSaveReferenceInternal(type))
            {
                return HeapDeresolve(serializedObject, resolver);
            }
            else
            {
                object instance = resolver.ManagedObjectOf(serializedObject);
                DeresolveInternal(serializedObject, ref instance, resolver);
                return instance;
            }
        }

        internal XElement HeapResolve(object resolveObject, string conventionType)
        {
            Guid finalGuid;
            //Метод GetCreateGuid(object,out Guid) возвращает bool значение которого отвечает на вопрос: 
            //Был ли создан новый Guid для этого объекта
            if (HeapManager.GetCreateGuid(resolveObject, out finalGuid))
                //Для объекта resolveObject уже создан Guid методом GetCreateGuid
                //Поэтому теперь мы обновляем сериализированное состояние объекта и добавляем его в кучу сериализированных объектов
                HeapManager.Serialized.ReplaceValue(finalGuid, ResolveInternal(resolveObject, conventionType));

            //В случае если объект resolveObject существует в куче, предпологается что он также 
            //существет и в куче сериализированных объектов.
            //В конечном счете, в итоговой xml-узел, в качестве значения, будет помещен guid указывающий на объект resolveObject
            return XMLUtility.CreateReferenceNode(conventionType, finalGuid.ToString());
        }
        internal object HeapDeresolve(XElement serialized, IResolver resolver)
        {
            object instance;
            Guid finalGuid;

            if (XMLUtility.GUIDAttributeConatins(serialized))
            {
                finalGuid = new Guid(XMLUtility.GuidOfAttributeInternal(serialized));
                instance = resolver.ManagedObjectOf(serialized);
                if (instance != null)
                    HeapManager.Managed.Add(finalGuid, instance);
                DeresolveInternal(serialized, ref instance, resolver);
            }
            else
            {
                finalGuid = new Guid(XMLUtility.GuidOfValueInternal(serialized));
                instance = HeapManager.Managed.ValueOf(finalGuid);
                if (instance == null)
                {
                    instance = AutoDeresolve(HeapManager.Serialized.ValueOf(finalGuid)); ;
                }
            }

            return instance;
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
            ResolversManager.GetResolver(conventionType).Serialize(serializeHere, resolveObject);
        }
        internal XElement ResolveInternal(object resolveObject, string convention, IResolver resolver)
        {
            if (resolveObject == null)
                return XMLUtility.CreateNullNode();

            var serialized = XMLUtility.CreateNode(convention);
            ResolveToInternal(resolveObject, resolver, serialized);
            return serialized;

        }
        internal XElement ResolveInternal(object resolveObject, string convention)
        {
            if (resolveObject == null)
                return XMLUtility.CreateNullNode();

            var serialized = XMLUtility.CreateNode(convention);
            ResolveToInternal(resolveObject, convention, serialized);
            return serialized;

        }
        internal XElement Resolve(object resolveObject)
        {
            if (resolveObject == null)
                return XMLUtility.CreateNullNode();

            var convention = TypeManager.ConventionOf(resolveObject.GetType());
            var serialized = XMLUtility.CreateNode(convention);
            ResolveToInternal(resolveObject, convention, serialized);
            return serialized;
        }
        /// <summary>
        /// Десериализирует xml-узел serializedObject в возвращаймый объект, используя действительный сериализатор для типа объекта serializedObject.
        /// Не допускается значение null ни для какаих входных параметров.
        /// Метод является контекстным.
        /// Обращается к банку resolver`ов для получения resolver`а 
        /// </summary>
        /// <param name="serializedObject">Десериализируемый объект</param>
        /// <param name="convention">Условное обозночение типа</param>
        /// <returns>Результат десериализации</returns>
        internal void DeresolveInternal(XElement serializedObject, ref object instance, string convention)
        {
            ResolversManager.GetResolver(convention).Deserialzie(serializedObject, ref instance);
        }
        /// <summary>
        /// Десериализирует xml-узел serializedObject в возвращаймый объект, используя указанный resolver.
        /// Не допускается значение null ни для какаих входных параметров.
        /// resolver должен соответствовать типу десериализирумого объекта.
        /// Метод является внеконтекстным, однако указаный в нем resolver может обращаться к контексту сериализатора (куче,словарю типов и тд).
        /// </summary>
        /// <param name="serializedObject">Десериализируемый объект</param>
        /// <param name="resoler">Resolver осуществляющий десериализацию</param>
        /// <returns>Результат десериализации</returns>
        internal void DeresolveInternal(XElement serializedObject, ref object instance, IResolver resolver)
        {
            resolver.Deserialzie(serializedObject, ref instance);
        }


    }
}
