using Colina.Language.Abstraction.Interfaces;
using Colina.Models.Abstraction.Actions;
using Colina.Models.Abstraction.DataTransferObjects;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colina.Language.CoreNLP.Analysers
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
                case "VB":
                    {
                        var commands = _cache.Get("commands") as List<CommandDto>;
                        if (commands.Any(c => c.EnUS.Equals(word.ToLower())))
                            userAction.ChangeCommand(word);
                        break;
                    }

                case "NN":
                    {
                        var images = _cache.Get("images") as List<ImageDto>;

                        var uniqueId = images.Where(i => i.EnUS.Equals(word.ToLower())).Select(i => i.UniqueId).FirstOrDefault();
                        if (!Guid.Empty.Equals(uniqueId))
                        {
                            userAction.ChangeObject(uniqueId);
                            return;
                        }

                        TryRecognizeDirection(word, ref userAction);
                        break;
                    }

                case "CD":
                    {
                        userAction.ChangeRelativePosition(int.Parse(word), default(Direction));
                        break;
                    }

                case "RB":
                case "IN":
                    {
                        TryRecognizeDirection(word, ref userAction);
                        break;
                    }                
                default: break;
            }            
        }

        private void TryRecognizeDirection(string word, ref UserAction userAction)
        {
            Direction direction = default(Direction);
            if (Enum.TryParse(word, true, out direction))
                userAction.ChangeRelativePositionDirection(direction);
        }
    }    
}