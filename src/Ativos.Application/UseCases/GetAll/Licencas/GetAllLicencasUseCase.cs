using Ativos.Communication.responses;
using Ativos.Communication.responses.Chamados;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Repositories.Licencas;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll.Licencas;

public class GetAllLicencasUseCase : IGetAllLicencasUseCase
{
    private readonly ILicencasReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllLicencasUseCase(ILicencasReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseLicencasJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseLicencasJson
        {
            Licencas = _mapper.Map<List<ResponseShortLicencaJson>>(result)
        };
    }
}