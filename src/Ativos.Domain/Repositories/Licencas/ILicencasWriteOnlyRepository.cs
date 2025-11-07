using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Licencas;

public interface ILicencasWriteOnlyRepository
{
    public Task Add(Licenca licenca);
}