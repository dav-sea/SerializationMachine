using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine
{
    public sealed class GUIDFactory : IGUIDFactory
    {
        public Guid TakeGUID()
        {
            return Guid.NewGuid();
        }
    }
}
