using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Domain.Repositories;
using Colina.Models.Abstraction.Actions;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.util;
using java.io;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static edu.stanford.nlp.ling.CoreAnnotations;

namespace Colina.Language.CoreNLP.Recognizers
{
    public class SentenceRecognizer : ISentenceRecognizer
    {
        private readonly ILanguageSettings _settings;
        private readonly IPartOfSpeechAnalyser _analyser;
        private IReadOnlyList<string> _annotators => new List<string>()
        {
            "tokenize", "ssplit", "pos" //, lemma, ner, parse, dcoref
        };

        public SentenceRecognizer(ILanguageSettings settings, IPartOfSpeechAnalyser analyser)
        {
            _settings = settings;
            _analyser = analyser;
        }

        public UserAction Recognize(string sentence)
        {
            var pipeline = GetCoreNLP();

            var annotation = new Annotation(sentence);

            pipeline.annotate(annotation);
            
            var sentences = (ArrayList) annotation.get(typeof(SentencesAnnotation));

            var userAction = default(UserAction);

            foreach (CoreMap phrase in sentences)
            {
                foreach (CoreLabel token in (ArrayList) phrase.get(typeof(TokensAnnotation)))
                {
                    var word = token.get(typeof(TextAnnotation)) as string;
                    var pos = token.get(typeof(PartOfSpeechAnnotation)) as string;
                    var ne = token.get(typeof(NamedEntityTagAnnotation)) as string;

                    _analyser.Analyse(word, pos, ref userAction);
                }
            }
            return userAction;
        }

        private StanfordCoreNLP GetCoreNLP()
        {
            var taggerRootPath = _settings.TaggerPath;
            var properties = GetProperties();

            var currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(taggerRootPath);

            var pipeline = new StanfordCoreNLP(properties);
            Directory.SetCurrentDirectory(currentDirectory);

            return pipeline;
        }

        private Properties GetProperties()
        {
            var props = new Properties();

            props.setProperty("annotators", string.Join(" ", _annotators));
            props.setProperty("ner.useSUTime", "0");

            return props;
        }

        // this is the parse tree of the current sentence
        //Tree tree = phrase.get(typeof(TreeAnnotation)) as Tree;
        // this is the Stanford dependency graph of the current sentence
        //SemanticGraph dependencies = (SemanticGraph)phrase.get(typeof(CollapsedCCProcessedDependenciesAnnotation));
    }
}