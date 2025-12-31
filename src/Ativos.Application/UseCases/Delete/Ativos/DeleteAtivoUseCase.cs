using Ativos.Domain;
using Ativos.Domain.Repositories;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Delete.Ativos;

public class DeleteAtivoUseCase : IDeleteAtivoUseCase
{
    private readonly IAtivosWriteOnlyRepository _writeRepository;
    private readonly IAtivosReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteAtivoUseCase(IAtivosWriteOnlyRepository writeRepository, IAtivosReadOnlyRepository readRepository, IUnitOfWork unitOfWork,  ILoggedUser loggedUser)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var ativos = await _readRepository.GetById(loggedUser, id);

        if(ativos is null) throw new NotFoundException("NOT FOUND");
        
        await _writeRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}