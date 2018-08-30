using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeMachine.Utility.Factory.Generic
{
    public interface IFactory<T>
    {
        T Instantiate();
    }
    public interface IFactoryArg<T, TArg>
    {
        T Instantiate(TArg arg);
    }
}
