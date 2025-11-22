using System.Net;

namespace Ativos.Exception.ExceptionsBase;

public class InvalidLoginException : AtivosException
{
    public InvalidLoginException() : base("Matricula ou senha invalida")
    {

    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}