using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Models.Abstraction.Actions
{
    public class Command
    {
        public string Identifier { get; set; }

        public Command(string identifier)
        {
            if (string.IsNullOrEmpty(identifier)) throw new ArgumentException(nameof(identifier));
            Identifier = identifier;
        }

        public Command()
        {

        }
    }
}
