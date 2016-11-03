using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public class PaletteObject
    {
        public Guid Identifier { get; private set; }

        public PaletteObject(Guid identifier)
        {
            if (Guid.Empty.Equals(identifier)) throw new ArgumentException(nameof(identifier));

            Identifier = identifier;
        }
        public PaletteObject()
        {

        }
    }
}
