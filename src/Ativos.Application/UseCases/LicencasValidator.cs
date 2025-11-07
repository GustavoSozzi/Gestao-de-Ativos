using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class LicencasValidator : AbstractValidator<RequestLicencasJson>
{
    public LicencasValidator()
    {
        RuleFor(licenca => licenca.Tipo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(licenca => licenca.Data).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(licenca => licenca.Nome_Soft).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
    }

}