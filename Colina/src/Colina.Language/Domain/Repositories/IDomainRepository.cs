using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.Domain.Repositories
{
    public interface IDomainRepository
    {
        void RetrieveCommands();
    }
}
