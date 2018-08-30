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
    public interface IFactoryArg
    {
        object Instatiate(object arg);
    }
    public interface IFactoryArg<TArg>
    {
        object Instatiate(TArg arg);
    }
}
