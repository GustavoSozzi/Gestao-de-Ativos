using Ativos.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{

    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>())).Returns("sidjdh#$%543S");
    }

    public PasswordEncripterBuilder Verify(string? password)
    {
        if (string.IsNullOrWhiteSpace(password) == false)
            _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>())).Returns(true);
        
        return this;
    }
    
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
    
    public IPasswordEncripter Builder() => _mock.Object;
}