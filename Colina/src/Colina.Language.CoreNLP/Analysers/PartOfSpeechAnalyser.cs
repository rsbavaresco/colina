using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Domain;
using Colina.Language.Domain.Repositories;
using Colina.Models.Abstraction.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.CoreNLP.Analysers
{
    public class PartOfSpeechAnalyser : IPartOfSpeechAnalyser
    {
        private readonly IDomainRepository _domainRepository;
        public PartOfSpeechAnalyser(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Analyse(string word, string pos, ref UserAction userAction)
        {

            _domainRepository.RetrieveCommands();

            userAction = userAction ?? new UserAction();            

            if (string.IsNullOrEmpty(word)) throw new ArgumentException(nameof(word));
            if (string.IsNullOrEmpty(pos)) throw new ArgumentException(nameof(pos));
            
            switch (pos)
            {
                case "VB":

                    if (EnglishDomain.AvaiableCommands.Contains(word))
                        userAction.ChangeCommand(word);
                     break;

                case "NN":
                    var index = EnglishDomain.AvaiablePaletteObjects.ToList().IndexOf(word.ToLower());
                    if (index >= 0)
                        userAction.ChangeObject(EnglishDomain.AvaiablePaletteObjectsIds[index]);                    
                    break;
                
                ///... TODO
                default: break;
            }            
        }
    }    
}
