using Ativos.Domain;

namespace Ativos.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AtivosDbContext _DbContext;

    public UnitOfWork(AtivosDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task Commit()
    {
        try
        {
            await _DbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
    //public async Task Commit() => await _DbContext.SaveChangesAsync();
}