using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Chamados;

public interface IChamadosReadOnlyRepository
{
    Task<List<Entities.Chamado>> GetAll(Usuario usuario, 
        string? nome = null, 
        string? modelo = null, 
        string? tipo = null, 
        long? codInventario = null, 
        string? cidade = null, 
        string? estado = null, 
        long? matriculaUsuario = null, 
        string? nomeUsuario = null);
    
    Task<List<Chamado>> FilterByMonth(Usuario usuario, DateOnly date);
}