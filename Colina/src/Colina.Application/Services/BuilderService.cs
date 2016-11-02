using Colina.Language.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Application.Services
{
    public class BuilderService
    {
        private readonly ISentenceRecognizer _sentenceRecognizer;
        public BuilderService(ISentenceRecognizer sentenceRecognizer)
        {
            _sentenceRecognizer = sentenceRecognizer;
        }

        public void Build(string sentence)
        {
            _sentenceRecognizer.Recognize(sentence);
        }
    }
}
