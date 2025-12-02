using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class UsuariosRepository : IUsuariosWriteOnlyRepository, IUsuariosReadOnlyRepository, IUsuariosUpdateOnlyReposiitory
{
    private readonly AtivosDbContext _dbContext;
    
    public UsuariosRepository(AtivosDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Usuario usuario)
    {
        await _dbContext.Usuario.AddAsync(usuario);
    }

    public async Task<List<Usuario>> GetAll(long? matricula = null, string? nome = null, 
        string? departamento = null, string? cargo = null, string? role = null)
    {
        var query = _dbContext.Usuario.AsQueryable();

        // Filtros de busca
        if (matricula.HasValue)
            query = query.Where(u => u.Matricula == matricula.Value);

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(u => u.P_nome.Contains(nome) || u.Sobrenome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(departamento))
            query = query.Where(u => u.Departamento != null && u.Departamento.Contains(departamento));

        if (!string.IsNullOrWhiteSpace(cargo))
            query = query.Where(u => u.Cargo != null && u.Cargo.Contains(cargo));

        if (!string.IsNullOrWhiteSpace(role))
            query = query.Where(u => u.Role == role);

        return await query.ToListAsync();
    }

    public async Task<bool> ExistActiveUserMatricula(int matricula)
    {
        return await _dbContext.Usuario.AnyAsync(user => user.Matricula.Equals(matricula)); //contem matricula
    }

    public async Task<Usuario?> GetUserByMatricula(int matricula)
    {
        return await _dbContext.Usuario.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Matricula.Equals(matricula));
    }

    public async Task<Usuario?> GetById(long idUsuario)
    {
        return await _dbContext.Usuario
            .Include(u => u.licencas)  // Carrega as licenças junto
            .FirstOrDefaultAsync(usuario => usuario.Id_usuario == idUsuario);
    }

    public void Update(Usuario usuario)
    {
        _dbContext.Usuario.Update(usuario);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Usuario.FirstOrDefaultAsync(usuario => usuario.Id_usuario == id);

        if(result is null)
        {
            return false;
        }

        _dbContext.Usuario.Remove(result);

        return true;
    }
}