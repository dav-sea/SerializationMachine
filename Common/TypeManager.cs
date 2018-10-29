using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Managers
{
    public sealed class TypeManager //: Core.ITypeManager
    {
        public TypeDictionary UsingDictionary { private set; get; }
        public TypeDictionary ReservedDictionary { private set; get; }

        public Type TypeOf(string convention)
        {
            Type type;
            if (!UsingDictionary.TryGetType(convention, out type))
                throw new ArgumentException("convention");
            return type;
        }

        public string ConventionOf(Type type)
        {
            string convention;
            if (!UsingDictionary.TryGetConvention(type, out convention))
            {
                convention = GetNewValidConvention(type);
                UsingDictionary.AddConventionInternal(type, convention);
            }
            return convention;
        }
        private string GetNewValidConvention(Type type)
        {
            string convention;
            if (!ReservedDictionary.TryGetConvention(type, out convention))
            {
                convention = CreateConvention(UsingDictionary.Count);
            }
            return convention;
        }
        private string CreateConvention(int number)
        {
            var result = "_" + Convert.ToString(number,16);
            if (UsingDictionary.ContainsConvention(result))
                return CreateConvention(number + 1);
            return result;
        }

        public void ReserveConvention(Type type,string convention)
        {
            if (type == null) throw new ArgumentNullException();
            if (string.Empty.Equals(convention)) throw new ArgumentException();

            ReservedDictionary.SetConventionInternal(type, convention);
        }

        public TypeManager(TypeDictionary dictionary)
        {
            this.UsingDictionary = dictionary;
            ReservedDictionary = new TypeDictionary(10);
        }
        internal TypeManager()
            : this(new TypeDictionary(20))
        {
    
        }
    }
}
