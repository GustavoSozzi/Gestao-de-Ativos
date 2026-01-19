using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases.Update.Usuarios;

public class UpdateUsuariosValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUsuariosValidator()
    {
        RuleFor(usuarios => usuarios.P_Nome).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(usuarios => usuarios.Sobrenome).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(usuarios => usuarios.Matricula)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.FIELD_REQUIRED).When(usuario => usuario.Matricula > 0);
    }
}
