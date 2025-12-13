using Ativos.Domain.Repositories.Localizacao;
using Moq;

namespace CommonTestUtilities.Repositories;

public class LocalizacaoWriteOnlyRepositoryBuilder
{
    public static ILocalizacaoWriteOnlyRepository Build()
    {
        var mock = new Mock<ILocalizacaoWriteOnlyRepository>();
        
        return mock.Object;
    }
}