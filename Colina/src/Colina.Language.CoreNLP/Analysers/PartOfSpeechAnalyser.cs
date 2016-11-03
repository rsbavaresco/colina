using Colina.Language.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colina.Models.Abstraction.Actions;
using Colina.Language.Domain;

namespace Colina.Language.CoreNLP.Analysers
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
