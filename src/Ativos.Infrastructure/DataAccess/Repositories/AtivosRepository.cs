using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class AtivosRepository : IAtivosReadOnlyRepository, IAtivosWriteOnlyRepository, IAtivosUpdateOnlyRepository
{
    //encapsulam o acesso a dados. Eles escondem os detalhes do banco de dados e expõem métodos para adicionar, buscar, atualizar ou remover entidades
    private readonly AtivosDbContext _dbContext;

    public AtivosRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Ativo ativo)
    {
        await _dbContext.Ativos.AddAsync(ativo);
    }

    public async Task<List<Ativo>> GetAll(string? nome = null, string? modelo = null, string? tipo = null, 
        long? codInventario = null, string? cidade = null, string? estado = null, 
        long? matriculaUsuario = null, string? nomeUsuario = null)
    {
        var query = _dbContext.Ativos
            .Include(a => a.localizacao)
            .Include(a => a.Usuario)
            .AsQueryable();

        // Filtros de busca
        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(a => a.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(modelo))
            query = query.Where(a => a.Modelo.Contains(modelo));

        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(a => a.Tipo != null && a.Tipo.Contains(tipo));

        if (codInventario.HasValue)
            query = query.Where(a => a.CodInventario == codInventario.Value);

        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(a => a.localizacao.Cidade.Contains(cidade));

        if (!string.IsNullOrWhiteSpace(estado))
            query = query.Where(a => a.localizacao.Estado.Contains(estado));

        if (matriculaUsuario.HasValue)
            query = query.Where(a => a.Usuario != null && a.Usuario.Matricula == matriculaUsuario.Value);

        if (!string.IsNullOrWhiteSpace(nomeUsuario))
            query = query.Where(a => a.Usuario != null && 
                (a.Usuario.P_nome.Contains(nomeUsuario) || a.Usuario.Sobrenome.Contains(nomeUsuario)));

        return await query.ToListAsync();
    }

    public async Task<Ativo?> GetById(long id)
    {
        return await _dbContext.Ativos.AsNoTracking().FirstOrDefaultAsync(ativo => ativo.Id_ativo == id);
    }

    public void Update(Ativo ativo)
    {
        _dbContext.Ativos.Update(ativo);
    }
    
    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Ativos.FirstOrDefaultAsync(ativo => ativo.Id_ativo == id);

        if(result is null)
        {
            return false;
        }

        _dbContext.Ativos.Remove(result);

        return true;
    }
}
