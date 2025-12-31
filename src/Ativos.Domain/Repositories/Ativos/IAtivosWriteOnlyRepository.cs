using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories;

public interface IAtivosWriteOnlyRepository
{
    public Task Add(Ativo ativo);
    Task Delete(long id);
}