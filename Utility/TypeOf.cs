using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Utility
{
    public static class TypeOf<T>
    {
        public static readonly Type Type = typeof(T);
        public static bool Equals(Type other)
        {
            return Type == other;
        }
    }
}
