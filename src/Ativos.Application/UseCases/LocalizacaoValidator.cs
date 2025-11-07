using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class LocalizacaoValidator : AbstractValidator<RequestLocalizacaoJson>
{
    public LocalizacaoValidator()
    {
        RuleFor(localizacao => localizacao.Cidade).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(localizacao => localizacao.Estado).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
    }
}