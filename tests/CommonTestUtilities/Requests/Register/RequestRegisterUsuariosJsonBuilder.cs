using Ativos.Communication.Requests;
using Bogus;
using Bogus.DataSets;

namespace CommonTestUtilities.Requests.Register;

public class RequestRegisterUsuariosJsonBuilder
{
    public static RequestUsuariosJson Build()
    {
        return new Faker<RequestUsuariosJson>()
            .RuleFor(r => r.P_nome, faker => faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(r => r.Sobrenome, faker => faker.Name.LastName())
            .RuleFor(r => r.Cargo, faker => faker.Random.String(10))
            .RuleFor(r => r.Departamento, faker => faker.Random.String(10))
            .RuleFor(r => r.Matricula, _ => 5050)
            .RuleFor(r => r.Password, faker => PasswordCustom(faker.Internet, 8, 16));
    }
    
    public static string PasswordCustom(Internet internet, int minLength, int maxLength) {
        var r = internet.Random;
        var validSymbols = new[] { '!', '?', '*', '.' };

        var lowercase = r.Char('a', 'z').ToString(); //minusculos
        var uppercase = r.Char('A', 'Z').ToString(); //maiusculos
        var number    = r.Char('0', '9').ToString(); //numeros
        var symbol    = r.ArrayElement(validSymbols).ToString(); //simbolos validos
        var padding   = r.String2(minLength - 4, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        var padding2  = r.String2(r.Number(0, maxLength - minLength), "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

        var chars = (lowercase + uppercase + number + symbol + padding + padding2).ToArray();
        var shuffledChars = r.Shuffle(chars).ToArray();

        return new string(shuffledChars);
    }
}