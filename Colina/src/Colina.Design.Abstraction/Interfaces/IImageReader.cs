using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Design.Abstraction.Interfaces
{
    public interface IImageReader
    {
        byte[] Read(string path);
    }
}
