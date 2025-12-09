using Ativos.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUsuariosWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsuariosWriteOnlyRepository>();
        
        return mock.Object;
    }
}