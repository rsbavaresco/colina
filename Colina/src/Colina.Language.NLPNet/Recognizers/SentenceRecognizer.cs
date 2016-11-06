using Colina.Language.Abstraction.Interfaces;
using Colina.Models.Abstraction.Actions;
using System.Diagnostics;
using Colina.Language.NLPNet.Settings;

namespace Colina.Language.NLPNet.Recognizers
{
    public class SentenceRecognizer : ISentenceRecognizer
    {
        private readonly IPartOfSpeechAnalyser _analyser;
        private readonly NLPNetSettings _settings;

        public SentenceRecognizer(IPartOfSpeechAnalyser analyser, NLPNetSettings settings)
        {
            _analyser = analyser;
            _settings = settings;
        }

        public UserAction Recognize(string sentence)
        {
            var userAction = default(UserAction);

            var tokens = RunPythonScript(sentence);

            foreach (var token in tokens.Split(' '))
            {
                var parts = token.Split('_');

                var word = parts[0];
                var pos = parts[1];

                _analyser.Analyse(word, pos, ref userAction);
            }

            return userAction;
        }

        private string RunPythonScript(string sentence)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(_settings.PythonPath)
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = $"{_settings.PythonAppPath} pos --data {_settings.ModelsPath} --lang pt"
                }
            };

            // start the process 
            process.Start();

            using (var streamWriter = process.StandardInput)
            {
                if (streamWriter.BaseStream.CanWrite)
                {
                    streamWriter.WriteLine(sentence);
                }
            }

            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            var streamReader = process.StandardOutput;
            string result = streamReader.ReadLine();

            // wait exit signal from the app we called and then close it. 
            process.WaitForExit();
            process.Close();

            return result ?? "";
        }
    }
}
