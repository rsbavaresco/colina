using System;
using Colina.Models.Abstraction.Actions;

namespace Colina.Language.Factories
{
    public static class DrawingFactory
    {
        public static Models.Abstraction.Designs.Drawing Create(UserAction userAction)
        {
            if (userAction == null)
                throw new ArgumentNullException(nameof(userAction));

            var position = GetPosition(userAction);

            return new Models.Abstraction.Designs.Drawing(position, userAction.Object);
        }

        private static Models.Abstraction.Designs.Position GetPosition(UserAction userAction)
        {
            if (userAction.Position == null)
                return new Models.Abstraction.Designs.Position(0, 0);

            if (userAction.Position is RelativePosition)
                return GetRelativePosition(userAction.Position as RelativePosition);
            else if (userAction.Position is AbsolutePosition)
                return GetAbsolutePosition(userAction.Position as AbsolutePosition);
            else
                throw new ArgumentException(nameof(userAction));
        }

        private static Models.Abstraction.Designs.Position GetRelativePosition(RelativePosition position)
        {
            throw new NotImplementedException();
        }

        private static Models.Abstraction.Designs.Position GetAbsolutePosition(AbsolutePosition position)
        {
            throw new NotImplementedException();
        }
    }
}
