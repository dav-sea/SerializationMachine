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
            return type.FullName;
        }
        public Type TypeOf(string convention)
        {
            return Type.GetType(convention);
        }

        public ConventionConvertor()
        {
            
        }
    }
}
