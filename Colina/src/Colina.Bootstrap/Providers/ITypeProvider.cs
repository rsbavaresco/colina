using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Abstraction.Bootstrap.Providers
{
    internal interface ITypeProvider 
    {
        T Provides<T>() where T : class;
    }
}
