using Colina.Models.Abstraction.Common;
using System;

namespace Colina.Models.Abstraction.Designs
{
    public class Drawing
    {
        public Position Position { get; private set; }
        public PaletteObject Object { get; private set; }
        public bool IsDeleted { get; private set; }

        public Drawing(Position position, PaletteObject paletteObject, bool isDeleted = false)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (paletteObject == null) throw new ArgumentNullException(nameof(paletteObject));

            Position = position;
            Object = paletteObject;
        }

        public Drawing(int x, int y, PaletteObject paletteObject, bool isDeleted = false)
        {            
            if (paletteObject == null) throw new ArgumentNullException(nameof(paletteObject));

            Position = new Position(x, y);
            Object = paletteObject;
        }
    }
}
