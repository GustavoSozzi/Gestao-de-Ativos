using Ativos.Communication.Requests;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class LicencasValidator : AbstractValidator<RequestLicencasJson>
{
    public LicencasValidator()
    {
        RuleFor(licenca => licenca.Tipo_Licenca).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED).IsInEnum().WithMessage("Campo informado invalido");
        RuleFor(licenca => licenca.Data).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED).
            Must(DateTimeIsValid).WithMessage("Data de Licenca invalida");
        RuleFor(licenca => licenca.Nome_Soft).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
    }
    
    private bool DateTimeIsValid(DateTime dataLicenca)
    {
        var pastLimit = DateTime.UtcNow.AddMonths(-1);
        
        if (dataLicenca.Date > DateTime.Now.Date) return false;
        if (dataLicenca.Date <  pastLimit) return false;

        return true;
    }

}