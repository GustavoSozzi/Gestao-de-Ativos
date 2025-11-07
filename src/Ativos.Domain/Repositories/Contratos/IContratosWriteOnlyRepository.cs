using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Contratos;

public interface IContratosWriteOnlyRepository
{
    public Task Add(Contrato contrato);
}