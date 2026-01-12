using Ativos.Domain.Enums;
using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;

public class LicenceBuilder
{
    public static List<Licenca> Collection(Usuario usuario, uint count = 2)
    {
        var list = new List<Licenca>();

        if (count == 0) count = 1;

        var licenceId = 1;

        for (int i = 0; i < count; i++) {
            var license = Build(usuario);
            license.Id_Licenca = licenceId++;
            
            list.Add(license);
        }

        return list;
    }
    
    public static Licenca Build(Usuario? usuario = null)
    {
        return new Faker<Licenca>()
            .RuleFor(r => r.Id_Licenca, _ => 3)
            .RuleFor(r => r.Tipo_Licenca, faker => faker.PickRandom<TipoLicenca>())
            .RuleFor(r => r.Data, faker => faker.Date.Recent());
    }

    public static Licenca BuildWithId(long id)
    {
        return new Faker<Licenca>()
            .RuleFor(r => r.Id_Licenca, _ => id)
            .RuleFor(r => r.Tipo_Licenca, faker => faker.PickRandom<TipoLicenca>())
            .RuleFor(r => r.Data, faker => faker.Date.Recent());
    }
}