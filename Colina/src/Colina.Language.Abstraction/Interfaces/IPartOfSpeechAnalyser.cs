using Colina.Models.Abstraction.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.Abstraction.Interfaces
{
    public interface IPartOfSpeechAnalyser
    {
        void Analyse(string word, string pos, ref UserAction userAction);
    }
}
