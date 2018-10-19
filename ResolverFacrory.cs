using System;
using System.Collections.Generic;
using SerializationMachine.Core;
using System.Reflection;
using System.Runtime;
using SerializationMachine.Resolvers;
using SerializationMachine.Utility;
using SerializationMachine.Resolvers.Primitives;
using SerializationMachine.Resolvers.BuiltIn;
using System.Runtime.Serialization;

namespace SerializationMachine
{
    public sealed class ResolverFacrory
    {
        private readonly Serializator Serializator;
        private static readonly Type RuntimeType = TypeOf<Type>.Type.GetType();//TODO DEBUG MB REMOVE .GetType();
        private static readonly Type Delegate = typeof(Delegate);

        /// <summary>
        /// Creates the resolver.
        /// </summary>
        /// <returns>The resolver.</returns>
        /// <param name="convention">Convention.</param>
        public IResolver CreateResolver(string convention)
        {
            var type = Serializator.GetTypeManager().TypeOf(convention);
            return CreateResolver(type);
        }

        public IResolver CreateResolver(Type type)
        {
            IResolver result;

            if (TryCreateDefaultResolver(type, out result))
                return result;

            if (TypeOf<ISerializable>.Type.IsAssignableFrom(type))
                return CreateISerialzableResolver(type);

            return CreateRuntimeResolver(type);
        }

        /// <summary>
        /// Tries the create default resolver.
        /// </summary>
        /// <returns><c>true</c>, if create default resolver was tryed, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="resolver">Resolver.</param>
        private bool TryCreateDefaultResolver(Type type,out IResolver resolver)
        {
            if (type.IsValueType)//Проверяем является ли тип значимым
            {
                if (type.IsPrimitive && TryCreatePrimitiveResolver(type, out resolver))//Проверяем является ли тип примитивным
                    return true;

                if (TypeOf<Decimal>.Equals(type)) 
                {
                    resolver = new DecimalResolver();
                    return true;
                }
            }
            else
            {
                if (type.IsArray)
                {
                    resolver = CreateArrayResolver(type);
                    return true;
                }

                if (TypeOf<Object>.Equals(type)) { resolver = new ObjectResolver(); return true; }
                if (TypeOf<String>.Equals(type)) { resolver = new StringResolver(); return true; }
                if (RuntimeType.Equals(type)) { resolver = new TypeResolver(Serializator); return true; }

                if (Delegate.IsAssignableFrom(type))
                {
                    if ((Serializator.Options & SerializatorOption.ThrowOutExceptions) != 0)
                        throw new NotSupportedException("Serialization delegates is not supported");
                    resolver = new EmptyResolver(Serializator);
                    return true;
                }

            }

            resolver = null;
            return false;
        }

        /// <summary>
        /// Creates the IS erialzable resolver.
        /// </summary>
        /// <returns>The IS erialzable resolver.</returns>
        /// <param name="type">Type.</param>
        private IResolver CreateISerialzableResolver(Type type)
        {
            if((Serializator.Options & SerializatorOption.AllowISerialzable) != 0)
                return new SerializableResolver(type, Serializator);
            return ExceptionHandler("Not Allow ISerialzble");
        }

        /// <summary>
        /// Creates the runtime resolver.
        /// </summary>
        /// <returns>The runtime resolver.</returns>
        /// <param name="type">Type.</param>
        private IResolver CreateRuntimeResolver(Type type)
        {
            if ((Serializator.Options & SerializatorOption.AllowRuntimeResolvers) != 0)
                return RuntimeResolver.ConfigurateRuntimeResolver(type, Serializator);
            return ExceptionHandler("Not Allow RuntimeResolvers");
        }


        /// <summary>
        /// Обработчик исключительных ситуаций
        /// </summary>
        /// <returns>EmptyResolver</returns>
        /// <param name="message">Exception message.</param>
        private IResolver ExceptionHandler(string message)
        {
            if ((Serializator.Options & SerializatorOption.ThrowOutExceptions) != 0)
                throw new NotSupportedException(message);
            else
                return new EmptyResolver(Serializator);
        }

        /// <summary>
        /// Пытется найти подходящий resolver для указанного примитивного типа 
        /// </summary>
        /// <returns><c>true</c>, if create primitive resolver was tryed, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="resolver">Resolver.</param>
        private bool TryCreatePrimitiveResolver(Type type, out IResolver resolver)
        {
            if (TypeOf<Boolean>.Equals(type)) { resolver = new BooleanResolver(); return true; }
            if (TypeOf<Byte>.Equals(type)) { resolver = new ByteResolver(); return true; }
            if (TypeOf<Char>.Equals(type)) { resolver = new CharResolver(); return true; }
            if (TypeOf<Double>.Equals(type)) { resolver = new DoubleResolver(); return true; }
            if (TypeOf<Int16>.Equals(type)) { resolver = new Int16Resolver(); return true; }
            if (TypeOf<Int32>.Equals(type)) { resolver = new Int32Resolver(); return true; }
            if (TypeOf<Int64>.Equals(type)) { resolver = new Int64Resolver(); return true; }
            if (TypeOf<SByte>.Equals(type)) { resolver = new SByteResolver(); return true; }
            if (TypeOf<Single>.Equals(type)) { resolver = new SingleResolver(); return true; }
            if (TypeOf<UInt16>.Equals(type)) { resolver = new UInt16Resolver(); return true; }
            if (TypeOf<UInt32>.Equals(type)) { resolver = new UInt32Resolver(); return true; }
            if (TypeOf<UInt64>.Equals(type)) { resolver = new UInt64Resolver(); return true; }
            resolver = null;
            return false;
        }
        /// <summary>
        /// Создает наиболее подходящий resolver для сериализации заданного типа массива.
        /// 
        /// Исключения : NotSupportedException* - массив имеет размерность больше чем один
        /// * Исключение возникает только с указанным SerializatorOption.ThrowOutExceptions
        /// 
        /// </summary>
        /// <returns>
        /// SimpleArrayResolver - если ранг массива равен единице
        /// EmptyResolver - если ранг массива болше чем один (если не выбран SerializatorOption.ThrowOutExceptions)
        /// </returns>
        /// <param name="type">Тип массива</param>
        private IResolver CreateArrayResolver(Type type)
        {
            var rank = type.GetArrayRank();
            if (rank == 1)
            {
                return new SimpleArrayResolver(type, Serializator);
            }
            return ExceptionHandler("Arrays with rank more 1 is not suppoted");//TODO
        }
        

        public ResolverFacrory(Serializator serializator)
        {
            this.Serializator = serializator;
        }
        
    }
}
