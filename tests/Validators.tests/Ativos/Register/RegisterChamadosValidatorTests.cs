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
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Error_Date_Past()
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Data_Abertura = DateTime.UtcNow.AddMonths(-1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("Data de abertura invalida"));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Titulo = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Theory]
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Description_Empty(string description)
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Descricao = description;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public void Error_Invalid_Status()
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Status_Chamado = (StatusChamado)500;

        var result = validator.Validate(request);
        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public void Error_Active_Mandatory()
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = -1;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Ativo é obrigatório"));
    }

    [Fact]
    public void Error_Tag_Invalid()
    {
        var validator = new ChamadosValidator();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Tags.Add((Tag)1000);
        
        var result = validator.Validate(request);
        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("tag type not supported"));
        
    }
}