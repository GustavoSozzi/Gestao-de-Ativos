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

    public async Task<bool> ExistActiveUserMatricula(int matricula)
    {
        return await _dbContext.Usuario.AnyAsync(user => user.Matricula.Equals(matricula));
    }
}