using Ativos.Application.UseCases;
using Ativos.Exception;

namespace Validators.tests.Ativos;

public class RegisterAtivosValidatorTests
{
    [Fact]
    public void Sucess()
    {
        var validator = new AtivosValidator();
        var request = RequestRe.Build();
    }
}