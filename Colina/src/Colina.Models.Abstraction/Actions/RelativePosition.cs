using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public class RelativePosition : Position
    {
        public string Unity { get; set; }
        public double Value { get; set; }

        public RelativePosition(string unity, double value, Direction direction) 
            : base(direction)
        {
            Unity = unity;
            Value = value;
        }

        public RelativePosition() : base()
        {

        }

        public void ChangeValue(double value)
        {
            Value = value;
        }
    }
}
