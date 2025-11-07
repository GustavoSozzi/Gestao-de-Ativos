using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosWriteOnlyRepository
{
    public Task Add(Chamado chamado);
}