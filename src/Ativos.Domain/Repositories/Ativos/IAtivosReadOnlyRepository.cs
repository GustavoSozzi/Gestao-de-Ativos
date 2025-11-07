using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories;

public interface IAtivosReadOnlyRepository
{
    Task<List<Ativo>> GetAll();
    Task<Ativo> GetById(long id);
}