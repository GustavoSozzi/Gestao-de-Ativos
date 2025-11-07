using Ativos.Domain;
using Ativos.Domain.Repositories;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;

namespace Ativos.Application.UseCases.Delete.Ativos;

public class DeleteAtivoUseCase : IDeleteAtivoUseCase
{
    private readonly IAtivosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAtivoUseCase(IAtivosWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {

        var result = await _repository.Delete(id);

        if(result == false)
        {
            throw new NotFoundException("NOT FOUND");
        }

        await _unitOfWork.Commit();
    }
}