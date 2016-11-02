using Colina.Language.Abstraction.Interfaces;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.semgraph;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.util;
using java.io;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static edu.stanford.nlp.ling.CoreAnnotations;
using static edu.stanford.nlp.semgraph.SemanticGraphCoreAnnotations;
using static edu.stanford.nlp.trees.TreeCoreAnnotations;

namespace Colina.Language.Recognizers
{
    public class SentenceRecognizer : ISentenceRecognizer
    {
        private readonly ILanguageSettings _settings;
        public SentenceRecognizer(ILanguageSettings settings)
        {
            _settings = settings;
        }

        public void Recognize(string sentence)
        {
            var jarRoot = _settings.TaggerPath;

            var props = new Properties();
            //props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
            props.setProperty("annotators", "tokenize ssplit pos");
            props.setProperty("ner.useSUTime", "0");

            var curDir = Environment.CurrentDirectory;
            Directory.SetCurrentDirectory(jarRoot);
            var pipeline = new StanfordCoreNLP(props);
            Directory.SetCurrentDirectory(curDir);

            var annotation = new Annotation(sentence);
            pipeline.annotate(annotation);

            var sentenceAnnotators = new SentencesAnnotation();

            var sentences = annotation.get(sentenceAnnotators.getClass());

            foreach (CoreMap phrase in (ArrayList)sentences)
            {
                foreach (CoreLabel token in (ArrayList)phrase.get(typeof(TokensAnnotation)))
                {

                    string word = token.get(typeof(TextAnnotation)) as string;

                    string pos = token.get(typeof(PartOfSpeechAnnotation)) as string;

                    string ne = token.get(typeof(NamedEntityTagAnnotation)) as string;
                }
                
                // this is the parse tree of the current sentence
                Tree tree = phrase.get(typeof(TreeAnnotation)) as Tree;

                // this is the Stanford dependency graph of the current sentence
                SemanticGraph dependencies = (SemanticGraph)phrase.get(typeof(CollapsedCCProcessedDependenciesAnnotation));
            }
        }
    }
}