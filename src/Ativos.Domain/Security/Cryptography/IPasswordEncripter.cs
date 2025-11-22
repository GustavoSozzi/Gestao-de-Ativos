namespace Ativos.Domain.Security.Cryptography;

public interface IPasswordEncripter
{
    string Encrypt(string password);
    public bool Verify(string password, string passworHash);
}