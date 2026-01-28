using Ativos.Domain.Entities;
using Ativos.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;

public class ChamadosBuilder
{
    public static List<Chamado> Collection(Ativo ativo, uint count = 2)
    {
        var list = new List<Chamado>();

        if (count == 0) count = 1;

        var chamadoId = 1;

        for (int i = 0; i < count; i++) {
            var chamados = Build(ativo);
            chamados.Id_Chamado = chamadoId++;
            
            list.Add(chamados);
        }

        return list;
    }
    
    public static Chamado Build(Ativo? ativo = null)
    {
        if (ativo is null) {
            var user = UserBuilder.Build();
            ativo = AtivosBuilder.Build(user);
        }
        
        var chamados = new Faker<Chamado>()
            .RuleFor(r => r.Id_Chamado, _ => 2)
            .RuleFor(r => r.Data_Abertura, _ => DateTime.Now)
            .RuleFor(r => r.Titulo, faker => faker.Random.String(10))
            .RuleFor(r => r.Descricao, faker => faker.Lorem.Sentence(3, 10))
            .RuleFor(r => r.Status_Chamado, faker => faker.PickRandom<StatusChamado>())
            .RuleFor(r => r.Tags, faker => faker.Make(1, () => new Ativos.Domain.Entities.Tag
            {
                Id = 1,
                Value = faker.PickRandom<Ativos.Domain.Enums.Tag>(),
                ChamadoId = 1
            }))
            .RuleFor(r => r.id_Ativo, _ => ativo.Id_ativo)
            .RuleFor(r => r.Ativo, _ => ativo);
            
        return chamados.Generate();
    }
}