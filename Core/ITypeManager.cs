using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Core
{
    public interface ITypeManager
    {
        TypeDictionary Dictionary { get; }
        Type TypeOf(string convention);
        string ConventionOf(Type type);
    }
}
