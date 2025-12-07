using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class UsuariosValidator : AbstractValidator<RequestUsuariosJson>
{
    public UsuariosValidator()
    {
        RuleFor(usuarios => usuarios.P_nome).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(usuarios => usuarios.Sobrenome).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Cargo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Departamento).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Matricula).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED).Must(IsValidRegistration).WithMessage("matricula invalida");
        RuleFor(usuarios => usuarios.Password).SetValidator(new PasswordValidator<RequestUsuariosJson>());
    }

    private bool IsValidRegistration(int registration)
    {
        if (registration > 0) return true;

        return false;
    }
}