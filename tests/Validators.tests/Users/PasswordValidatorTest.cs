using Ativos.Application.UseCases;
using Ativos.Communication.Requests;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using FluentValidation;

namespace Validators.tests.Ativos;

public class PasswordValidatorTest
{
    [Theory] //passando dados 
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    [InlineData("aaa")]
    [InlineData("aaaaa")]    
    [InlineData("aaaaaaaa")]    
    [InlineData("AAAAAAAA")]    
    [InlineData("Aaaaaaaa")]    
    [InlineData("Aaaaaaa1")]
    [InlineData("!!!!@@@!")]
    public void Error_Password_Invalid(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RequestUsuariosJson>();
        //Act
        var result = validator.IsValid(new ValidationContext<RequestUsuariosJson>(new RequestUsuariosJson()), password);

        //Assert
        result.Should().BeFalse();
    }
}