namespace Ativos.Exception.ExceptionsBase;

public abstract class AtivosException : SystemException
{
    protected AtivosException(string message) : base(message) 
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
