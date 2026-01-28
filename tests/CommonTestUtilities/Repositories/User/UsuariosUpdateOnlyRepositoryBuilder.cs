using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UsuariosUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUsuariosUpdateOnlyReposiitory> _repository;

    public UsuariosUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsuariosUpdateOnlyReposiitory>();
    }
    
    public static UsuariosUpdateOnlyRepositoryBuilder Build() => new UsuariosUpdateOnlyRepositoryBuilder();
    
    public UsuariosUpdateOnlyRepositoryBuilder WithUser(Usuario usuario)
    {
        _repository
            .Setup(r => r.GetById(usuario.Id_usuario))
            .ReturnsAsync(usuario);

        return this;
    }

    public IUsuariosUpdateOnlyReposiitory BuildRepository() => _repository.Object;
}