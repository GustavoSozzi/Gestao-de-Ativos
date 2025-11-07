using Ativos.Communication.responses;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Localizacao;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll;

public class GetAllLocalizacoUseCase : IGetAllLocalizacaoUseCase
{
    private readonly ILocalizacaoReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllLocalizacoUseCase(ILocalizacaoReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseLocalizacaoJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseLocalizacaoJson()
        {
            Localizacoes = _mapper.Map<List<ResponseShortLocalizacaoJson>>(result)
        };
    }
}