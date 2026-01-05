using Ativos.Domain.Entities;

namespace WebApi.Test.Resources;

public class AtivosIdentityManager
{
    private Ativo _ativo;

    public AtivosIdentityManager(Ativo ativo)
    {
        _ativo = ativo;
    }
    
    public long GetAtivoId() => _ativo.Id_ativo;
}