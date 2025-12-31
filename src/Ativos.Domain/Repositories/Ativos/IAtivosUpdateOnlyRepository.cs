namespace Ativos.Domain.Entities;

public interface IAtivosUpdateOnlyRepository
{
    Task<Ativo> GetById(Usuario usuario, long id);
    
    void Update(Ativo ativo);
}