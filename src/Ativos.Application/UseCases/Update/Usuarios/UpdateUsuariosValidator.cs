using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases.Update.Usuarios;

public class UpdateUsuariosValidator : AbstractValidator<RequestUsuariosJson>
{
    public UpdateUsuariosValidator()
    {
        RuleFor(usuarios => usuarios.P_nome).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(usuarios => usuarios.Sobrenome).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Cargo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Departamento).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Matricula).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        
        // Password é opcional no update, mas se fornecido, deve ser válido
        When(usuarios => !string.IsNullOrWhiteSpace(usuarios.Password), () =>
        {
            RuleFor(usuarios => usuarios.Password).SetValidator(new PasswordValidator<RequestUsuariosJson>());
        });
    }
}
