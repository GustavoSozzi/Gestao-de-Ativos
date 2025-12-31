using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosUpdateOnlyRepository
{
    Task<Chamado> GetById(Usuario usuario, long id);
    
    void Update(Chamado chamado);
}