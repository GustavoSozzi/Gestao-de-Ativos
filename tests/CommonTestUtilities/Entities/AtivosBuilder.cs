using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;

public class AtivosBuilder
{

    public static List<Ativo> Collection(Usuario usuario, uint count = 2)
    {
        var list = new List<Ativo>();

        if (count == 0) count = 1;

        var ativoId = 1;

        for (int i = 0; i < count; i++) {
            var ativo = Build(usuario);
            ativo.Id_ativo = ativoId++;
            
            list.Add(ativo);
        }

        return list;
    }
    
    
    public static Ativo Build(Usuario usuario)
    {
        var ativos = new Faker<Ativo>()
            .RuleFor(r => r.Id_ativo, _ => 19)
            .RuleFor(r => r.id_localizacao, _ => 1)
            .RuleFor(r => r.Nome, faker => faker.Name.FullName())
            .RuleFor(r => r.Modelo, faker => faker.Lorem.Text())
            .RuleFor(r => r.SerialNumber, faker => faker.Random.String())
            .RuleFor(r => r.CodInventario, faker => faker.Random.Number(1, 10000))
            .RuleFor(r => r.Tipo, faker => faker.Lorem.Text())
            .RuleFor(r => r.id_usuario, _ => usuario.Id_usuario);

        return ativos.Generate(); // ✅ Adicionar .Generate() para criar o objeto

    }
}