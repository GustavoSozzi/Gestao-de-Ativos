using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories;

public interface IAtivosReadOnlyRepository
{
    Task<List<Ativo>> GetAll(string? nome = null, string? modelo = null, string? tipo = null, 
        long? codInventario = null, string? cidade = null, string? estado = null, 
        long? matriculaUsuario = null, string? nomeUsuario = null);
    Task<Ativo> GetById(long id);
}