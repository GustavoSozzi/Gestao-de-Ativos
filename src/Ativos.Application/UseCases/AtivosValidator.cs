using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class AtivosValidator : AbstractValidator<RequestAtivosJson>
{
    public AtivosValidator()
    {
        RuleFor(ativos => ativos.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(ativos => ativos.Modelo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(ativos => ativos.SerialNumber).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(ativos => ativos.CodInventario).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED)
            .Must(CodeIsValid).WithMessage("Campo de codigo invalido");
        RuleFor(ativos => ativos.Tipo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
    }

    private bool CodeIsValid(long code)
    {
        if (code <= 0) return false;

        return true;
    }
}