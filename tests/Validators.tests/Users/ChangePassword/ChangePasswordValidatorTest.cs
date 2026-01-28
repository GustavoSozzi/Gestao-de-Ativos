using Ativos.Application.UseCases.Users.ChangePassword;
using Ativos.Communication.Requests;
using Ativos.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.tests.Ativos.ChangePassword;

public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordJsonBuilder.Build();
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Error_NewPassword_Empty(string newPassword)
    {
        var validator = new ChangePasswordValidator();
        
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = newPassword;
        
        var result = validator.Validate(request);
        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
    }
}