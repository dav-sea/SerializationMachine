using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Reflection;
using System.Runtime;
using SerializeMachine.Resolvers;
using SerializeMachine.Utility;
using SerializeMachine.Resolvers.Primitives;
using SerializeMachine.Resolvers.BuiltIn;
using SerializeMachine.Utility.Factory.Generic;

namespace SerializeMachine
{
    public sealed class ResolverFacrory
    {
        private readonly Serializator Serializator;
        private readonly Type RuntimeType = TypeOf<Type>.Type.GetType();

        public IResolver CreateResolver(string convention)
        {
            var type = Serializator.TypeManager.TypeOf(convention);
            IResolver result;

            //Нужно отловить все втроенные типы
            if (type.IsValueType)//Проверяем является ли тип значимым
            {
                if (type.IsPrimitive )//Проверяем является ли тип примитивным
                {
                    if(TryCreatePrimitiveResolver(type,out result)) return result;
                }
                if (TypeOf<Decimal>.Equals(type)) return new DecimalResolver();
            }
            else
            {
                if (type.IsArray)
                    return CreateArrayResolver(type);

                if (TypeOf<Object>.Equals(type)) return new ObjectResolver();
                if (TypeOf<String>.Equals(type)) return new StringResolver();
                if (RuntimeType.Equals(type)) return new TypeResolver(Serializator);
            }

            if (TypeOf<System.Runtime.Serialization.ISerializable>.Type.IsAssignableFrom(type))
            {
                return new SerializableResolver(type, Serializator);
            }

            return RuntimeResolver.ConfigurateRuntimeResolver(type, Serializator);
        }

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
        private IResolver CreateArrayResolver(Type type)
        {
            var rank = type.GetArrayRank();
            if (rank == 1)
            {
                return new SimpleArrayResolver(type, Serializator);
            }
            else throw new InvalidOperationException();
        }
        

        public ResolverFacrory(Serializator serializator)
        {
            this.Serializator = serializator;
        }
        
    }
}
