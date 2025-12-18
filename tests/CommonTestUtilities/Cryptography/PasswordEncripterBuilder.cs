using Ativos.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("!%dyWahdhd@");
        mock.Setup(passwordEncripter => passwordEncripter.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        
        return mock.Object;
    }

    public static IPasswordEncripter BuildWithWrongPassword()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("!%dyWahdhd@");
        mock.Setup(passwordEncripter => passwordEncripter.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
        
        return mock.Object;
    }
}