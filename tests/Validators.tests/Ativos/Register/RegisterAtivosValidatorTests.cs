using Ativos.Application.UseCases;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace Validators.tests.Ativos;

public class RegisterAtivosValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory] //passando dados 
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Nome = name;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Model_Empty(string model)
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Modelo = model;

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
    public void Error_SerialNumber_Empty(string serialNumber)
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.SerialNumber = serialNumber;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public void Error_CodInventario_Empty()
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.CodInventario = -1000;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Campo de codigo invalido"));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Type_Empty(string type)
    {
        //Arrange
        var validator = new AtivosValidator();
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Tipo = type;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
}