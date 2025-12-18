using System.Net;

namespace Ativos.Exception.ExceptionsBase;

public class InvalidLoginException : AtivosException
{
    public InvalidLoginException() : base("Matricula ou senha invalidas")
    {

    }

    public InvalidLoginException(string message) : base(message)
    {

    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}