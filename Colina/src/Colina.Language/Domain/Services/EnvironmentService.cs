using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Domain.Repositories;
using Colina.Models.Abstraction.Common;
using Colina.Models.Abstraction.DataTransferObjects;
using Colina.Models.Abstraction.Designs;
using System.Linq;

namespace Colina.Language.Domain.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IEnvironmentRepository _environmentRepository;

        public EnvironmentService(IEnvironmentRepository environmentRepository)
        {
            _environmentRepository = environmentRepository;
        }

        public Environment Handle(System.Guid userSession, Drawing drawing)
        {
            var environmentDto = _environmentRepository.GetByUserSession(userSession);

            var envItem = environmentDto.Items.SingleOrDefault(x => x.ImageUniqueId == drawing.Object.Identifier);

            if (envItem == null) // Novo objeto
            {
                var newItem = EnvironmentItemDto.Create(
                    drawing.Object.Identifier, 
                    drawing.Position.X, 
                    drawing.Position.Y
                );
                environmentDto.AddItem(newItem);
            }
            else // Objeto da paleta já utilizado, então atualizam-se suas posições
            {
                envItem.PositionX = drawing.Position.X;
                envItem.PositionY = drawing.Position.Y;
            }

            // Atualiza o Environment
            _environmentRepository.Update(environmentDto);

            return new Environment(environmentDto.SessionId)
            {
                Drawings = (from ei in environmentDto.Items
                            select new Drawing(
                                ei.PositionX,
                                ei.PositionY,
                                new PaletteObject(ei.ImageUniqueId)
                            )).ToList()
            };
        }
    }
}
