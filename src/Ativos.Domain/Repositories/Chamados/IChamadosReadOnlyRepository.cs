using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosReadOnlyRepository
{
    Task<List<Entities.Chamado>> GetAll();
    Task<List<Chamado>> FilterByMonth(DateOnly date);
}