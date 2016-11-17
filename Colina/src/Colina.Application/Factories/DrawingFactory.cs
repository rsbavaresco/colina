using Colina.Models.Abstraction.Actions;
using Colina.Models.Abstraction.Designs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.Factories
{
    public static class DrawingFactory
    {
        public static Drawing Create(UserAction userAction)
        {
            if (userAction.Position == null)
                return new Drawing(0, 0, userAction.Object);
            return null;
        }
    }
}
