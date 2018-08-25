using System;
using System.Reflection;

namespace SerializeMachine.Utility
{
    public static class Factory
    {
       /* public static Func<object> CreateFactoryByFunc(Type objectType, InstantiateMode instantiateMode)
        {
            return CreateFactory(objectType, instantiateMode).Instantiate;
        }
        public static IFactory CreateFactory(Type objectType,InstantiateMode instantiateMode)
        {
            if (objectType == null) return new NullFactory();

            switch (instantiateMode)
            {
                case InstantiateMode.Constructor:
                    return CreateConstructorFactory(SerializationUtility.Reflection.GetDefaultConstructor(objectType));
                case InstantiateMode.OnlyAllocatedMemory:
                    return CreateUninitializedFactory(objectType);
            }

            return new NullFactory();
        }
        */
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
