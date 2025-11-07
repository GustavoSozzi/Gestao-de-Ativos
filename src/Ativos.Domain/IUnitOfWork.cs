namespace Ativos.Domain;

public interface IUnitOfWork
{
    Task Commit();
}