using System.Net;

namespace Ativos.Exception.ExceptionsBase;

public class NotFoundException : AtivosException
{
    public NotFoundException(string message) : base(message){}

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}