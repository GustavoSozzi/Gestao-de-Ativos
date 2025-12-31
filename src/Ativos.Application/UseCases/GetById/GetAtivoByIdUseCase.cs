using Ativos.Communication.responses;
using Ativos.Communication.responses.Usuarios;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.GetById;

public class GetAtivoByIdUseCase : IGetAtivoByIdUseCase
{
    private readonly IAtivosReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetAtivoByIdUseCase(IAtivosReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseAtivoJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        
        var result = await _repository.GetById(loggedUser, id);

        if(result is null)
            throw new NotFoundException("NOT FOUND");
        
        return _mapper.Map<ResponseAtivoJson>(result);
    }
}