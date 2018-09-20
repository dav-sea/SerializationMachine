using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Utility
{
    internal sealed class NullFactory : IFactory
    {
        public object Instantiate()
        {
            return null;
        }
    }
}
