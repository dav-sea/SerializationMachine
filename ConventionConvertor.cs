using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class ConventionConvertor
    {
        public string ConventionOf(Type type)
        {
            if (type.IsArray)
            {
                return type.FullName.Replace("[]", "__ARRAY");
            }
            return type.FullName;
        }
        public Type TypeOf(string convention)
        {
            return Type.GetType(convention.Replace("__ARRAY", "[]"));
        }

        public ConventionConvertor()
        {
            
        }
    }
}
