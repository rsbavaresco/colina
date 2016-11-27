using Colina.Design.Abstraction.Interfaces;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Factories;
using Colina.Language.ViewModels;
using System;

namespace Colina.Application.Services
{
    public class BuilderService
    {
        private readonly ISentenceRecognizer _sentenceRecognizer;
        private readonly IImageDrawer _drawer;
        private readonly IImageReader _reader;
        private readonly IEnvironmentService _environmentService;
        public BuilderService(
            ISentenceRecognizer sentenceRecognizer,
            IImageDrawer drawer, 
            IImageReader reader,
            IEnvironmentService environmentService)
        {
            _sentenceRecognizer = sentenceRecognizer;
            _drawer = drawer;
            _reader = reader;
            _environmentService = environmentService;
        }

        public BuilderViewModel Build(Guid sessionId, string sentence)
        {
            // Reconhece o comando do usuário
            var userAction = _sentenceRecognizer.Recognize(sentence);

            // Gera o objeto Drawing
            var drawing = DrawingFactory.Create(userAction);

            // Compõe o Environment de acordo com o Drawing
            var environment = _environmentService.Handle(sessionId, drawing);

            // TODO: Tratar casos de exclusão de objetos do ambiente
            
            // TODO: Desenhar o ambiente informando o arquivo .col para o drawer
            _drawer.Draw(environment);

            var bytes = _reader.Read($"{environment.SessionId}.col");

            return new BuilderViewModel(bytes, drawing.Object.Identifier);
        }
    }
}
