using Ativos.Domain;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;

namespace Ativos.Application.UseCases.Delete.Usuarios;

public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUsuarioUseCase(IUsuariosWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if(result == false)
        {
            throw new NotFoundException("Usuário não encontrado");
        }

        await _unitOfWork.Commit();
    }
}
