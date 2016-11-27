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

        public Position GetPaletteObjectPosition(System.Guid userSession, System.Guid paletteObject)
        {
            var environment = _environmentRepository.GetByUserSession(userSession);

            if (environment == null)
                return null;

            var envItem = environment.Items.SingleOrDefault(x => x.ImageUniqueId == paletteObject);
            if (envItem == null)
                return null;

            return new Position(envItem.PositionX, envItem.PositionY);
        }

        public Environment Handle(System.Guid userSession, Drawing drawing)
        {
            bool isNew;
            var environmentDto = CreateIfNotExists(userSession, out isNew);

            CreateOrUpdateEnvironmentItem(ref environmentDto, drawing);

            if (isNew)
                _environmentRepository.Insert(environmentDto);
            else
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

        private EnvironmentDto CreateIfNotExists(System.Guid userSession, out bool isNew)
        {
            var environmentDto = _environmentRepository.GetByUserSession(userSession);
            isNew = environmentDto == null;
            return environmentDto ?? EnvironmentDto.Create(userSession);
        }

        private void CreateOrUpdateEnvironmentItem(ref EnvironmentDto environment, Drawing drawing)
        {
            var envItem = GetEnvironmentItemByPaletteObject(environment, drawing.Object.Identifier);

            if (envItem == null && !drawing.IsDeleted) // Novo objeto
            {
                var newItem = EnvironmentItemDto.Create(
                    drawing.Object.Identifier,
                    drawing.Position.X,
                    drawing.Position.Y
                );
                environment.AddItem(newItem);
            }
            else // Objeto da paleta já utilizado
            {
                if (drawing.IsDeleted)
                {
                    environment.RemoveItem(envItem);
                }
                else
                {
                    envItem.PositionX = drawing.Position.X;
                    envItem.PositionY = drawing.Position.Y;
                }
            }
        }

        private EnvironmentItemDto GetEnvironmentItemByPaletteObject(EnvironmentDto environment, System.Guid identifier)
        {
            return environment.Items.SingleOrDefault(x => x.ImageUniqueId == identifier);
        }
    }
}
