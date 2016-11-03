using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public class AbsolutePosition : Position
    {
        public string Quadrant { get; set; }

        public AbsolutePosition(string quadrant, Direction direction)
            : base(direction)
        {
            if (string.IsNullOrEmpty(quadrant)) throw new ArgumentException(nameof(quadrant));

            Quadrant = quadrant;
        }

        public AbsolutePosition()
        {

        }
    }
}
