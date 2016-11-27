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
                    else
                    {
                        Direction direction;
                        TryRecognizeDirection(word, out direction);
                        userAction.ChangeRelativePositionDirection(direction);
                    }
                        
                    break;

                case "NUM":
                    userAction.ChangeRelativePosition(int.Parse(word), default(Direction));
                    break;

                default: break;
            }
        }

        private void TryRecognizeDirection(string word, out Direction direction)
        {
            switch (word.ToLower())
            {
                case "direita":
                    direction = Direction.Right;
                    break;

                case "esquerda":
                    direction = Direction.Left;
                    break;

                case "cima":
                    direction = Direction.Up;
                    break;

                case "baixo":
                    direction = Direction.Down;
                    break;

                default:
                    direction = default(Direction);
                    break;
            }
        }
    }
}
