using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Reflection;
using System.Runtime;
using SerializeMachine.Resolvers;
using SerializeMachine.Utility;
using SerializeMachine.Resolvers.Primitives;
using SerializeMachine.Resolvers.BuiltIn;

namespace SerializeMachine
{
    public sealed class ResolverFacrory
    {
        private readonly Serializator Serializator;
        private static readonly Type RuntimeType = TypeOf<Type>.Type.GetType();

        public IResolver CreateResolver(string convention)
        {
            var type = Serializator.GetTypeManager().TypeOf(convention);

            //Нужно отловить все втроенные типы
            if (type.IsValueType)//Проверяем является ли тип значимым
            {
                if (type.IsPrimitive)//Проверяем является ли тип примитивным
                {
                    if (TypeOf<Boolean>.Equals(type)) return new BooleanResolver();
                    if (TypeOf<Byte>.Equals(type)) return new ByteResolver();
                    if (TypeOf<Char>.Equals(type)) return new CharResolver();
                    if (TypeOf<Double>.Equals(type)) return new DoubleResolver();
                    if (TypeOf<Int16>.Equals(type)) return new Int16Resolver();
                    if (TypeOf<Int32>.Equals(type)) return new Int32Resolver();
                    if (TypeOf<Int64>.Equals(type)) return new Int64Resolver();
                    if (TypeOf<SByte>.Equals(type)) return new SByteResolver();
                    if (TypeOf<Single>.Equals(type)) return new SingleResolver();
                    if (TypeOf<UInt16>.Equals(type)) return new UInt16Resolver();
                    if (TypeOf<UInt32>.Equals(type)) return new UInt32Resolver();
                    if (TypeOf<UInt64>.Equals(type)) return new UInt64Resolver();
                }
                if (TypeOf<Decimal>.Equals(type)) return new DecimalResolver();
            }
            else
            {
                if (type.IsArray)
                {
                    //var elementType = type.GetGenericArguments()[0];
                    var rank = type.GetArrayRank();
                    if (rank == 1)
                    {
                        return new SimpleArrayResolver(type, Serializator);
                    }
                    else throw new InvalidOperationException();
                }
                if (TypeOf<Object>.Equals(type))return new ObjectResolver();
                if (TypeOf<String>.Equals(type))return new StringResolver();
                if (RuntimeType.Equals(type)) return new TypeResolver(Serializator);
                
            }

            if (TypeOf<System.Runtime.Serialization.ISerializable>.Type.IsAssignableFrom(type))
            {
                return new SerializableResolver(type, Serializator);
            }

            return RuntimeResolver.ConfigurateRuntimeResolver(type, Serializator);
        }

        public ResolverFacrory(Serializator serializator)
        {
            this.Serializator = serializator;
        }
    }
}
