using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class UsuariosRepository : IUsuariosWriteOnlyRepository, IUsuariosReadOnlyRepository
{
    private readonly AtivosDbContext _dbContext;
    
    public UsuariosRepository(AtivosDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Usuario usuario)
    {
        await _dbContext.Usuario.AddAsync(usuario);
    }

    public async Task<List<Usuario>> GetAll()
    {
        return await _dbContext.Usuario.ToListAsync();
    }

    public async Task<bool> ExistActiveUserMatricula(int matricula)
    {
        return await _dbContext.Usuario.AnyAsync(user => user.Matricula.Equals(matricula)); //contem matricula
    }

    public async Task<Usuario?> GetUserByMatricula(int matricula)
    {
        return await _dbContext.Usuario.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Matricula.Equals(matricula));
    }
}