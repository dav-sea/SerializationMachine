using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Managers
{
    public sealed class TypeManager : Core.ITypeManager
    {
        public TypeDictionary Dictionary { private set; get; }
        public TypeDictionary InvalidTypeDictionary { private set; get; }

        public Type TypeOf(string convention)
        {
            Type type;
            if (!Dictionary.TryGetType(convention, out type))
                throw new ArgumentException("convention");
            return type;
        }

        public string ConventionOf(Type type)
        {
            string convention;
            if (!Dictionary.TryGetConvention(type, out convention))
            {
                convention = GetNewValidConvention(type);
                Dictionary.AddConvention(type, convention);
            }
            return convention;
        }

        private string GetNewValidConvention(Type type)
        {
            string convention;
            if (!InvalidTypeDictionary.TryGetConvention(type, out convention))
            {
                convention = CreateConvention(Dictionary.Count);
            }
            return convention;
        }

        private string CreateConvention(int number)
        {
            var result = "_" + Convert.ToString(number, 16);
            if (Dictionary.ContainsConvention(result))
                return CreateConvention(number + 1);
            return result;
        }

        public TypeManager(TypeDictionary dictionary)
        {
            this.Dictionary = dictionary;
            InvalidTypeDictionary = new TypeDictionary(10);
        }
        internal TypeManager()
            : this(new TypeDictionary(20))
        {
    
        }
    }
}
