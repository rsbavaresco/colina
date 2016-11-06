using Colina.Language.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.CoreNLP.Settings
{
    public class LanguageSettings : ILanguageSettings
    {
        private readonly StanfordSettings _stanford;
        public LanguageSettings(StanfordSettings stanford)
        {
            _stanford = stanford;
        }

        public string TaggerPath
        {
            get
            {
                return _stanford.ModelsPath;
            }
        }
    }
}
