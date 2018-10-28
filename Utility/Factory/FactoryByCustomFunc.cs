using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationMachine.Utility
{
    [Obsolete("Use TemplateInstanceFactory")]
    internal sealed class FactoryByCustomFunc : IFactory
    {
        private readonly Func<object> CustomFunc;

        internal FactoryByCustomFunc(Func<object> customFunc)
        {
            this.CustomFunc = customFunc;
        }

        public object Instantiate()
        {
            return CustomFunc();
        }
    }
}
