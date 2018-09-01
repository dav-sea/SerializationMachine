using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Managers
{
    public sealed class TypeManager : Core.ITypeManager
    {
        public TypeDictionary Dictionary
        {
            private set;
            get;
        }

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
                convention = CreateConvention(Dictionary.Count);
                Dictionary.AddConvention(type, convention);
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
        }
        internal TypeManager()
            : this(new TypeDictionary(50)) { }

    }
}
