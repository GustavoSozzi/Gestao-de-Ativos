namespace Ativos.Application.UseCases.Delete.Ativos;

public interface IDeleteAtivoUseCase
{
    Task Execute(long id);
}