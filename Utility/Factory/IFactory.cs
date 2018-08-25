using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Utility
{
    public interface IFactory
    {
        object Instantiate();
    }
}
