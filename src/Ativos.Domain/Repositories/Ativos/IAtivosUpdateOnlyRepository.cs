namespace Ativos.Domain.Entities;

public interface IAtivosUpdateOnlyRepository
{
    Task<Ativo> GetById(long id);
    
    void Update(Ativo ativo);
}