using Ativos.Domain.Repositories.Chamados;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ChamadosWriteOnlyRepositoryBuilder
{
    public static IChamadosWriteOnlyRepository Build()
    {
        var mock = new Mock<IChamadosWriteOnlyRepository>();
        
        return mock.Object;
    }
}