using Ativos.Domain;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;

namespace Ativos.Application.UseCases.Delete.Usuarios;

public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteUsuarioUseCase(IUsuariosWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
