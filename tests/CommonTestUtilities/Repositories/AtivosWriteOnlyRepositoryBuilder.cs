using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AtivosWriteOnlyRepositoryBuilder
{
    public static IAtivosWriteOnlyRepository Build()
    {
        var mock = new Mock<IAtivosWriteOnlyRepository>();
        
        return mock.Object;
    }
}