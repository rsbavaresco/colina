using Colina.Language.Abstraction.Interfaces;
using Colina.Models.Abstraction.Designs;
using System;

namespace Colina.Language.Factories
{
    public class DrawingFactory
    {
        private readonly IEnvironmentService _environmentService;
        private Position _existingPaletteObjectPosition;

        public DrawingFactory(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }

        public Drawing Create(Models.Abstraction.Actions.UserAction userAction, Guid userSession)
        {
            if (userAction == null)
                throw new ArgumentNullException(nameof(userAction));

            _existingPaletteObjectPosition = _environmentService.GetPaletteObjectPosition(userSession, userAction.Object.Identifier);

            var position = GetPosition(userAction);

            return new Drawing(position, userAction.Object);
        }

        private Position GetPosition(Models.Abstraction.Actions.UserAction userAction)
        {
            if (userAction.Position == null)
                return GetDefaultPosition();

            if (userAction.Position is Models.Abstraction.Actions.RelativePosition)
                return GetRelativePosition(userAction.Position as Models.Abstraction.Actions.RelativePosition);
            else if (userAction.Position is Models.Abstraction.Actions.AbsolutePosition)
                return GetAbsolutePosition(userAction.Position as Models.Abstraction.Actions.AbsolutePosition);
            else
                throw new ArgumentException(nameof(userAction));
        }

        private Position GetRelativePosition(Models.Abstraction.Actions.RelativePosition position)
        {
            if (_existingPaletteObjectPosition != null)
            {
                switch (position.Direction)
                {
                    case Models.Abstraction.Actions.Direction.Left:
                        return new Position(_existingPaletteObjectPosition.X - position.Value, _existingPaletteObjectPosition.Y);

                    case Models.Abstraction.Actions.Direction.Right:
                        return new Position(_existingPaletteObjectPosition.X + position.Value, _existingPaletteObjectPosition.Y);

                    case Models.Abstraction.Actions.Direction.Up:
                        return new Position(_existingPaletteObjectPosition.X, _existingPaletteObjectPosition.Y - position.Value);

                    case Models.Abstraction.Actions.Direction.Down:
                        return new Position(_existingPaletteObjectPosition.X, _existingPaletteObjectPosition.Y + position.Value);

                    default:
                        return GetDefaultPosition();
                }
            }

            return GetDefaultPosition();
        }

        private static Position GetAbsolutePosition(Models.Abstraction.Actions.AbsolutePosition position)
        {
            throw new NotImplementedException();
        }

        private static Position GetDefaultPosition()
        {
            return new Position(0, 0);
        }
    }
}
