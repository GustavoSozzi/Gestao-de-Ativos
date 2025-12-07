using Ativos.Application.UseCases;
using Ativos.Communication.Enums;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace Validators.tests.Ativos;

public class RegisterChamadosValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Error_Date_Past()
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Data_Abertura = DateTime.UtcNow.AddMonths(-1);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("Data de abertura invalida"));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Titulo = title;

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
    public void Error_Description_Empty(string description)
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Descricao = description;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public void Error_Invalid_Status()
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Status_Chamado = (StatusChamado)500;

        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("Campo informado invalido"));
    }
    
    [Fact]
    public void Error_Active_Mandatory()
    {
        //Arrange
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = -1;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Ativo é obrigatório"));
    }
}