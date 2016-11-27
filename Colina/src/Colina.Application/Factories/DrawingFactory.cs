using Colina.Models.Abstraction.Designs;
using System;
using Actions = Colina.Models.Abstraction.Actions;

namespace Colina.Language.Factories
{
    public static class DrawingFactory
    {
        public static Drawing Create(Actions.UserAction userAction)
        {
            if (userAction == null)
                throw new ArgumentNullException(nameof(userAction));

            var position = GetPosition(userAction);

            return new Drawing(position, userAction.Object);
        }

        private static Position GetPosition(Actions.UserAction userAction)
        {
            if (userAction.Position == null)
                return new Position(0, 0);

            if (userAction.Position is Actions.RelativePosition)
                return GetRelativePosition(userAction.Position as Actions.RelativePosition);
            else if (userAction.Position is Actions.AbsolutePosition)
                return GetAbsolutePosition(userAction.Position as Actions.AbsolutePosition);
            else
                throw new ArgumentException(nameof(userAction));
        }

        private static Position GetRelativePosition(Actions.RelativePosition position)
        {
            throw new NotImplementedException();
        }

        private static Position GetAbsolutePosition(Actions.AbsolutePosition position)
        {
            throw new NotImplementedException();

            switch (position.Direction)
            {
                //case Actions.Direction.Back:

                //    break;

                case Actions.Direction.Center:

                    break;

                case Actions.Direction.Down:

                    break;

                //case Actions.Direction.Front:

                //    break;

                case Actions.Direction.Up:
                    
                    break;

                default:
                    throw new ArgumentException(nameof(position));
            }
        }
    }
}
