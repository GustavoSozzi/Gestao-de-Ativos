using Ativos.Domain.Entities;
using Ativos.Domain.Enums;
using CommonTestUtilities.Cryptography;
using Bogus;

namespace CommonTestUtilities.Entities;

public class UserBuilder
{
    public static List<Usuario> Collection(uint count = 2)
    {
        var list = new List<Usuario>();
        var userTest = Build();
        var matricula = userTest.Matricula;
        
        if (count == 0) count = 1;

        var usuarioId = 1;

        for (int i = 0; i < count; i++) {
            var usuarios = Build();
            usuarios.Matricula = matricula++;
            usuarios.Id_usuario = usuarioId++;
            
            list.Add(usuarios);
        }

        return list;
    }
    
    public static Usuario Build(string role = Roles.TEAM_MEMBER)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();

        var user = new Faker<Usuario>()
            .RuleFor(u => u.Id_usuario, _ => 7)
            .RuleFor(r => r.P_nome, faker => faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(r => r.Sobrenome, faker => faker.Name.LastName())
            .RuleFor(r => r.Cargo, faker => faker.Random.String(10))
            .RuleFor(r => r.Departamento, faker => faker.Random.String(10))
            .RuleFor(r => r.Matricula, _ => 5050)
            .RuleFor(r => r.Password, _ => passwordEncripter.Encrypt("!Aa1password"))
            .RuleFor(r => r.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(u => u.Role, _ => role);

        return user;
    }
}