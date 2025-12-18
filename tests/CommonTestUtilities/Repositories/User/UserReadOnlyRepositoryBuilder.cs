using Ativos.Domain.Entities;
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

    public void ExistActiveUserMatricula(long matricula)
    {
        _repository.Setup(userReadOnly => userReadOnly.ExistActiveUserMatricula(matricula)).ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByMatricula(Usuario usuario)
    {
        _repository.Setup(userRepository => userRepository.GetUserByMatricula(usuario.Matricula)).ReturnsAsync(usuario);

        return this;
    }
    
    public UserReadOnlyRepositoryBuilder GetById(Usuario usuario)
    {
        _repository.Setup(userRepository => userRepository.GetById(usuario.Id_usuario)).ReturnsAsync(usuario);

        return this;
    }

    public IUsuariosReadOnlyRepository Build() => _repository.Object;
}