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
            var name = type.Name;

           
            return name.Replace("System.", "_SYS.").Replace("[]", "_ARR").Replace("`", "_OF_");
        }
        public Type TypeOf(string convention)
        {
            return Type.GetType(convention.Replace("_SYS.", "System.").Replace("_ARR", "[]").Replace("_OF_", "`"));
        }

        

        public ConventionConvertor()
        {
            
        }
    }
}
