using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace SerializationMachine.Utility
{
    public interface ITemplateInstanceFactory : IFactoryArg<XElement> { }
    public sealed class UninitializedInstanceFactory : ITemplateInstanceFactory , IFactory
    {
        public readonly Type InstanceType;
        public object Instantiate(XElement arg)
        {
            return SerializationUtility.InstantiateUninitializedObject(InstanceType);
        }

        object IFactory.Instantiate()
        {
            return Instantiate(null);
        }

        public UninitializedInstanceFactory(Type instanceType)
        {
            InstanceType = instanceType ?? throw new ArgumentNullException();
        }
    }
    public sealed class ConstructorInstanceFactory : ITemplateInstanceFactory, IFactory
    {
        public readonly ConstructorInfo Constructor;

        public object Instantiate(XElement arg)
        {
            return Constructor.Invoke(null);
        }
        object IFactory.Instantiate()
        {
            return Instantiate(null);
        }
        public ConstructorInstanceFactory(Type instanceType)
        {
            if (instanceType == null) throw new ArgumentNullException();
            Constructor = SerializationUtility.Reflection.GetDefaultConstructor(instanceType);
        }
    }
    public sealed class FuncInstanceFactory : ITemplateInstanceFactory
    {
        public readonly Func<XElement, object> FactoryFunc;
        public object Instantiate(XElement arg)
        {
            return FactoryFunc(arg);
        }
        public FuncInstanceFactory(Func<XElement, object> factoryFunc)
        {
            FactoryFunc = factoryFunc ?? throw new ArgumentNullException();
        }
    }
    public sealed class ActivatorInstanceFactory : ITemplateInstanceFactory,IFactory
    {
        public readonly Type InstanceType;
        public object Instantiate(XElement arg)
        {
            return Activator.CreateInstance(InstanceType);
        }

        object IFactory.Instantiate()
        {
            return Instantiate(null);
        }

        public ActivatorInstanceFactory(Type instanceType)
        {
            InstanceType = instanceType ?? throw new ArgumentNullException();
        }
    }

    public sealed class NullTemplateFactory : ITemplateInstanceFactory, IFactory
    {
        public object Instantiate(XElement arg)
        {
            return null;
        }

        object IFactory.Instantiate()
        {
            return null;
        }
    }
    public sealed class DefaultTemplateFactory : ITemplateInstanceFactory, IFactory
    {
        IFactory Factory;
        public object Instantiate(XElement arg)
        {
            return Factory.Instantiate();
        }

        object IFactory.Instantiate()
        {
            return Instantiate(null);
        }

        public DefaultTemplateFactory(Type templateType)
        {
            if (templateType.IsValueType)
                Factory = new ActivatorInstanceFactory(templateType);
            else Factory = new NullTemplateFactory();
        }
    }
}
