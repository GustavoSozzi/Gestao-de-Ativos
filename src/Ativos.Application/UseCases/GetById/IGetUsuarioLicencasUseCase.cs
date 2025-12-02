namespace Ativos.Application.UseCases.GetById;

public interface IGetUsuarioLicencasUseCase
{
    Task<List<long>> Execute(long idUsuario);
}
