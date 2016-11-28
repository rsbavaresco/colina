using Colina.Language.Abstraction.Interfaces;
using System;
using System.Linq;
using Colina.Models.Abstraction.Actions;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Colina.Models.Abstraction.DataTransferObjects;

namespace Colina.Language.NLPNet.Analysers
{
    public class PartOfSpeechAnalyser : IPartOfSpeechAnalyser
    {
        private readonly IMemoryCache _cache;
        public PartOfSpeechAnalyser(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void Analyse(string word, string pos, ref UserAction userAction)
        {
            userAction = userAction ?? new UserAction();

            if (string.IsNullOrEmpty(word)) throw new ArgumentException(nameof(word));
            if (string.IsNullOrEmpty(pos)) throw new ArgumentException(nameof(pos));

            switch (pos)
            {
                case "V":
                    var commands = _cache.Get("commands") as List<CommandDto>;
                    if (commands.Any(c => c.PtBR.Equals(word.ToLower())))
                        userAction.ChangeCommand(word);
                                        
                    break;

                case "N":
                    var images = _cache.Get("images") as List<ImageDto>;

                    var uniqueId = images.Where(i => i.PtBR.Equals(word.ToLower())).Select(i => i.UniqueId).FirstOrDefault();
                    if (!Guid.Empty.Equals(uniqueId))
                    {
                        userAction.ChangeObject(uniqueId);                        
                    }
                    else
                    {
                        Direction direction = default(Direction);
                        if (TryRecognizeDirection(word, out direction))
                            userAction.ChangeRelativePositionDirection(direction);
                    }
                        
                    break;

                case "NUM":
                    userAction.ChangeRelativePosition(int.Parse(word), default(Direction));
                    break;

                default: break;
            }
        }

        private bool TryRecognizeDirection(string word, out Direction direction)
        {
            bool recognized = true;
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
                    recognized = false;
                    break;
            }
            return recognized;
        }
    }
}
