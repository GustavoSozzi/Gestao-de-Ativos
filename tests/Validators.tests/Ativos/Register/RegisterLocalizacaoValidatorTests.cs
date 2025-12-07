using Ativos.Application.UseCases;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace Validators.tests.Ativos;

public class RegisterLocalizacaoValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new LocalizacaoValidator();
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_City_Empty(string city)
    {
        //Arrange
        var validator = new LocalizacaoValidator();
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        request.Cidade = city;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_State_Empty(string state)
    {
        //Arrange
        var validator = new LocalizacaoValidator();
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        request.Estado = state;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
}