using Ativos.Communication.responses;
using Ativos.Domain.Repositories;
using Ativos.Domain.Services.LoggedUser;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll;

public class GetAllAtivosUseCase : IGetAllAtivosUseCase
{
    private readonly IAtivosReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetAllAtivosUseCase(IAtivosReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseAtivosJson> Execute(string? nome = null, string? modelo = null, string? tipo = null, 
        long? codInventario = null, string? cidade = null, string? estado = null, 
        long? matriculaUsuario = null, string? nomeUsuario = null)
    {
        var loggedUser = await _loggedUser.Get();
        
        var result = await _repository.GetAll(loggedUser, nome, modelo, tipo, codInventario, cidade, estado, matriculaUsuario, nomeUsuario);

        return new ResponseAtivosJson { Ativos = _mapper.Map<List<ResponseShortAtivoJson>>(result)};
    }
}