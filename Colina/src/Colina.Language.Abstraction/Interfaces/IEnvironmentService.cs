using Colina.Models.Abstraction.Designs;

namespace Colina.Language.Abstraction.Interfaces
{
    public interface IEnvironmentService
    {
        Environment Handle(System.Guid userSession, Drawing drawing);
    }
}
