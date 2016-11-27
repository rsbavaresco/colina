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
        private readonly DrawingFactory _drawingFactory;

        public BuilderService(
            ISentenceRecognizer sentenceRecognizer,
            IImageDrawer drawer, 
            IImageReader reader,
            IEnvironmentService environmentService,
            DrawingFactory drawingFactory)
        {
            _sentenceRecognizer = sentenceRecognizer;
            _drawer = drawer;
            _reader = reader;
            _environmentService = environmentService;
            _drawingFactory = drawingFactory;
        }

        public BuilderViewModel Build(Guid sessionId, string sentence)
        {
            // Reconhece o comando do usuário
            var userAction = _sentenceRecognizer.Recognize(sentence);

            // Cria o objeto Drawing
            var drawing = _drawingFactory.Create(userAction, sessionId);

            // Compõe o Environment de acordo com o Drawing
            var environment = _environmentService.Handle(sessionId, drawing);

            // TODO: Tratar casos de exclusão de objetos do ambiente
            
            // Desenha o ambiente
            _drawer.Draw(environment);

            var bytes = _reader.Read($"{environment.SessionId}.png");

            return new BuilderViewModel(bytes, drawing.Object.Identifier);
        }
    }
}
