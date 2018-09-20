using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Utility.Factory.Generic
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
