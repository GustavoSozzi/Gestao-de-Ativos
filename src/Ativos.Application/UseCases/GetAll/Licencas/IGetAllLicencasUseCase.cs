using Ativos.Communication.responses;

namespace Ativos.Application.UseCases.GetAll.Licencas;

public interface IGetAllLicencasUseCase
{
    Task<ResponseLicencasJson> Execute();
}