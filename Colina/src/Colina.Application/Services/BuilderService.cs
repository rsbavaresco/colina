using Colina.Design.Abstraction.Interfaces;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Factories;
using Colina.Language.ViewModels;
using Colina.Models.Abstraction.Designs;

namespace Colina.Application.Services
{
    public class BuilderService
    {
        private readonly ISentenceRecognizer _sentenceRecognizer;
        private readonly IImageDrawer _drawer;
        private readonly IImageReader _reader;
        public BuilderService(ISentenceRecognizer sentenceRecognizer,
            IImageDrawer drawer, IImageReader reader)
        {
            _sentenceRecognizer = sentenceRecognizer;
            _drawer = drawer;
            _reader = reader;
        }

        public BuilderViewModel Build(string sentence)
        {
            var userAction = _sentenceRecognizer.Recognize(sentence);
            var drawing = DrawingFactory.Create(userAction);

            // TODO: Gerar arquivo .col

            // TODO: Manipular Environment do usuário (por sessão)
            var environment = new Environment();
            environment.AddDrawing(drawing);
            
            _drawer.Draw(environment);

            var bytes = _reader.Read("result.png");

            return new BuilderViewModel(bytes);
        }
    }
}
