using Colina.Language.Abstraction.Interfaces;
using System;
using System.Linq;
using Colina.Models.Abstraction.Actions;
using Colina.Language.Domain;

namespace Colina.Language.NLPNet.Analysers
{
    public class PartOfSpeechAnalyser : IPartOfSpeechAnalyser
    {
        public void Analyse(string word, string pos, ref UserAction userAction)
        {
            userAction = userAction ?? new UserAction();

            if (string.IsNullOrEmpty(word)) throw new ArgumentException(nameof(word));
            if (string.IsNullOrEmpty(pos)) throw new ArgumentException(nameof(pos));

            switch (pos)
            {
                case "V":

                    if (PortugueseDomain.AvaiableCommands.Contains(word))
                        userAction.ChangeCommand(word);
                    break;

                case "N":
                    var index = PortugueseDomain.AvaiablePaletteObjects.ToList().IndexOf(word.ToLower());
                    if (index >= 0)
                        userAction.ChangeObject(PortugueseDomain.AvaiablePaletteObjectsIds[index]);
                    break;

                ///... TODO
                default: break;
            }
        }
    }
}
