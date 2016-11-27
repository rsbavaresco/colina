using System.Collections.Generic;

namespace Colina.Models.Abstraction.Designs
{
    public class Environment
    {
        public ICollection<Drawing> Drawings { get; private set; }

        public void AddDrawing(Drawing drawing)
        {
            Drawings.Add(drawing);
        }
    }
}
