using System;
using System.Collections.Generic;

namespace Colina.Models.Abstraction.Designs
{
    public class Environment
    {
        public Environment(Guid sessionId)
        {
            this.SessionId = sessionId;
        }

        public Guid SessionId { get; set; }
        public IList<Drawing> Drawings { get; set; }

        public void AddDrawing(Drawing drawing)
        {
            Drawings.Add(drawing);
        }
    }
}
