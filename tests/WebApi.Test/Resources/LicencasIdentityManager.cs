using Ativos.Domain.Entities;

namespace WebApi.Test.Resources;

public class LicencasIdentityManager
{
    private Licenca _licenca;

    public LicencasIdentityManager(Licenca licenca)
    {
        _licenca = licenca;
    }
    public long GetLicencasId() => _licenca.Id_Licenca;
}