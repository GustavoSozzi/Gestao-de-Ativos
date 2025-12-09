using Ativos.Domain.Entities;
using Ativos.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<Usuario>())).Returns("token");
        
        return mock.Object;
    }
}