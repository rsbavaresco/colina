using Colina.Models.Abstraction.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public class UserAction
    {
        public Command Command { get; private set; }
        public PaletteObject Object { get; private set; }
        public Position Position { get; private set; }

        public void ChangeCommand(string command)
        {
            Command = new Command(command);
        }

        public void ChangeObject(Guid paletteObjectId)
        {
            Object = new PaletteObject(paletteObjectId);
        }

        public void ChangeAbsolutePosition(string quadrant, string direction)
        {
            if (string.IsNullOrEmpty(direction)) throw new ArgumentException(nameof(direction));

            Position = new AbsolutePosition(quadrant, (Direction)Enum.Parse(typeof(Direction), direction));
        }

        public void ChangeRelativePosition(double value, Direction direction)
        {
            var current = (Position as RelativePosition);

            if (current != null)
            {
                current.ChangeValue(value);
                current.ChangeDirection(direction);
                return;
            }

            Position = new RelativePosition("pixels", value, direction);                       
        }

        public void ChangeRelativePositionDirection(Direction direction)
        {
            if (Position == null)
                Position = new RelativePosition("pixels", default(double), direction);
            else
                Position.ChangeDirection(direction);
        }
    }
}