using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public abstract class Position
    {
        public Direction Direction { get; set; }

        public Position(Direction direction)
        {
            Direction = direction;
        }

        public Position()
        {

        }
    }
}
