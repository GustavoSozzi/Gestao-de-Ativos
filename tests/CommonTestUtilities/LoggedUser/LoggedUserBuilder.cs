using Ativos.Domain.Entities;
using Ativos.Domain.Services.LoggedUser;
using Moq;

namespace CommonTestUtilities.LoggedUser;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(Usuario usuario)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(usuario);
        
        return mock.Object;
    }
}