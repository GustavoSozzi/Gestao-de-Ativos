using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosReadOnlyRepository
{
    Task<List<Entities.Chamado>> GetAll(Usuario usuario);
    Task<List<Chamado>> FilterByMonth(Usuario usuario, DateOnly date);
}