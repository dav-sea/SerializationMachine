using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Utility
{
    internal sealed class NullFactory : IFactory
    {
        public object Instantiate()
        {
            return null;
        }
    }
}
