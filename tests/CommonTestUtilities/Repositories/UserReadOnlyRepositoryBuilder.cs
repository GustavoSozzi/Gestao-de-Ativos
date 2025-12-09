using Ativos.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsuariosReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsuariosReadOnlyRepository>();
    }

    public IUsuariosReadOnlyRepository Build() => _repository.Object;
}