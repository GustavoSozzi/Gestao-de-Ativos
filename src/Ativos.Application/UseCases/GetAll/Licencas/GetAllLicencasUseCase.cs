using Ativos.Communication.responses;
using Ativos.Communication.responses.Chamados;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Repositories.Licencas;
using Ativos.Domain.Services.LoggedUser;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll.Licencas;

public class GetAllLicencasUseCase : IGetAllLicencasUseCase
{
    private readonly ILicencasReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetAllLicencasUseCase(ILicencasReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseLicencasJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        
        var result = await _repository.GetAll(loggedUser);

        return new ResponseLicencasJson
        {
            Licencas = _mapper.Map<List<ResponseShortLicencaJson>>(result)
        };
    }
}