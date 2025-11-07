using Ativos.Communication.responses;
using Ativos.Domain.Repositories;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll;

public class GetAllAtivosUseCase : IGetAllAtivosUseCase
{
    private readonly IAtivosReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllAtivosUseCase(IAtivosReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseAtivosJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseAtivosJson
        {
            Ativos = _mapper.Map<List<ResponseShortAtivoJson>>(result)
        };
    }
}