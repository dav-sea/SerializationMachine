using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Utility
{
    [Obsolete("Use TemplateInstanceFactory")]
    internal sealed class NullFactory : IFactory
    {
        public object Instantiate()
        {
            return null;
        }
    }
}
