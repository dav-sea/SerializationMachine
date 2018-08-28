using System;
using System.Reflection;

namespace SerializeMachine.Utility
{
    public static class Factory
    {
        public static IFactory CreateConstructorFactory(ConstructorInfo constructor)
        {
            if (constructor == null) 
                return new NullFactory();
            return new FactoryByConstructor(constructor);
        }
        public static IFactory CreateUninitializedFactory(Type objectType)
        {
            if (objectType == null)
                return new NullFactory();
            return new UninitializedObjectFactory(objectType);
        }
        public static IFactory CreateConstructorFactory(Type objectType)
        {
            if (objectType == null)
                return new NullFactory();
            return CreateConstructorFactory(SerializationUtility.Reflection.GetDefaultConstructor(objectType));
        }
        public static IFactory CreateCustomFactory(Func<object> instantiator)
        {
            if (instantiator == null) return new NullFactory();
            return new FactoryByCustomFunc(instantiator);
        }
    }
}
