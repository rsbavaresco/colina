using Colina.Models.Abstraction.DataTransferObjects;
using System;

namespace Colina.Language.Domain.Repositories
{
    public interface IEnvironmentRepository
    {
        EnvironmentDto GetByUserSession(Guid session);
        void Insert(EnvironmentDto environment);
        void Update(EnvironmentDto environment);
    }
}
