using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosUpdateOnlyRepository
{
    Task<Chamado> GetById(long id);
    
    void Update(Chamado chamado);
}