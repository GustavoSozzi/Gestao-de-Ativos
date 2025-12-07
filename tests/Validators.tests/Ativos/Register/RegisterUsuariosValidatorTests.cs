using Ativos.Application.UseCases;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace Validators.tests.Ativos;

public class RegisterUsuariosValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        
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
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.P_nome = name;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Theory] //passando dados 
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_LastName_Empty(string lastName)
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.Sobrenome = lastName;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Theory] //passando dados 
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Load_Empty(string load)
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.Cargo = load;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Theory] //passando dados 
    [InlineData("")] 
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Department_Empty(string department)
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.Departamento = department;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public void Error_Registration_Empty()
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.Matricula = -1;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("matricula invalida"));
    }
    
    [Fact]
    public void Error_Invalid_Password()
    {
        //Arrange
        var validator = new UsuariosValidator();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.Password = "aaaaa";

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
    }
}