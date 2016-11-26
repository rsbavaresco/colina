using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Domain;
using Colina.Models.Abstraction.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Colina.Data.Repositories.DataTransfersObjects;

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
                            userAction.ChangeObject(uniqueId);
                        break;
                    }
                
                ///... TODO
                default: break;
            }            
        }
    }    
}