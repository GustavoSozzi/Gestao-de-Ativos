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
    
    public UsuariosUpdateOnlyRepositoryBuilder GetById(Usuario? usuarios)
    {
        _repository.Setup(usuarioRepository => usuarioRepository.GetById(usuarios.Id_usuario)).ReturnsAsync(usuarios);

        return this;
    }
    
    public IUsuariosUpdateOnlyReposiitory Build() => _repository.Object;
}