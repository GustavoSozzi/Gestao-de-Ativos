using Ativos.Communication.responses;
using Ativos.Communication.responses.Chamados;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Services.LoggedUser;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll.Chamados;

public class GetAllChamadosUseCase : IGetAllChamadosUseCase
{
    private readonly IChamadosReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetAllChamadosUseCase(IChamadosReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseChamadosJson> Execute()
    {
        var loggedUser =  await _loggedUser.Get();
        
        var result = await _repository.GetAll(loggedUser);

        return new ResponseChamadosJson
        {
            Chamados = _mapper.Map<List<ResponseShortChamadoJson>>(result)
        };
    }
}