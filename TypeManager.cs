using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class TypeManager
    {
        private readonly TypeDictionary TypeDictionary;
        private readonly ConventionConvertor ConventionConvertor;

        public string ConventionOf(Type type,bool dictionarySync = true)
        {
            string convention;
            if (!TypeDictionary.TryGetConvention(type, out convention))
            {
                convention = ConventionConvertor.ConventionOf(type);
                if (dictionarySync) TypeDictionary.AddConvention(type, convention);
            }
            return convention;
        }
        public Type TypeOf(string convention,bool dictionarySync = true)
        {
            Type type;
            if (!TypeDictionary.TryGetType(convention, out type))
            {
                type = ConventionConvertor.TypeOf(convention);
                if (dictionarySync) TypeDictionary.AddConvention(type, convention);
            }
            return type;
        }

        public TypeDictionary Dictionary { get { return TypeDictionary; } }
        public ConventionConvertor Convertor { get { return ConventionConvertor; } }

        public TypeManager(TypeDictionary typeDictionary)
        {
            this.TypeDictionary = typeDictionary;
            this.ConventionConvertor = new ConventionConvertor();
        }

        public TypeManager()
            :this(new TypeDictionary(50))
        {

        }
    }
}
