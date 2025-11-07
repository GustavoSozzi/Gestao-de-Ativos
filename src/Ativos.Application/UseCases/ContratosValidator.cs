using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class ContratosValidator : AbstractValidator<RequestContratosJson>
{
    public ContratosValidator()
    {
        RuleFor(contratos => contratos.Tipo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(contratos => contratos.Descricao).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(contratos => contratos.Valor).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
    }
}