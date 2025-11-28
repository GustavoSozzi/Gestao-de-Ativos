using Ativos.Communication.responses;
using Ativos.Communication.responses.Chamados;
using Ativos.Domain.Repositories.Chamados;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll.Chamados;

public class GetAllChamadosUseCase : IGetAllChamadosUseCase
{
    private readonly IChamadosReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllChamadosUseCase(IChamadosReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseChamadosJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseChamadosJson
        {
            Chamados = _mapper.Map<List<ResponseShortChamadoJson>>(result)
        };
    }
}