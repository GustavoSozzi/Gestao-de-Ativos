using Ativos.Application.UseCases;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace Validators.tests.Ativos;

public class RegisterLicencasValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new LicencasValidator();
        var request = RequestRegisterLicencasJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Error_Date_PastLicence()
    {
        //Arrange
        var validator = new LicencasValidator();
        var request = RequestRegisterLicencasJsonBuilder.Build();
        request.Data = DateTime.UtcNow.AddMonths(-1);
        request.Data = DateTime.MinValue;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2).
            And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED))
            .And.Contain(e => e.ErrorMessage.Equals("Data de Licenca invalida"));
    }
}