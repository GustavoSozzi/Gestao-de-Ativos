using Ativos.Communication.responses;
using Ativos.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ativos.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AtivosException)
            HandleProjectException(context);
        else
            ThrownUnknowError(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var ativosException = context.Exception as AtivosException;
        var errorResponse = new ResponseErrorJson(ativosException!.GetErrors());

        context.HttpContext.Response.StatusCode = ativosException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrownUnknowError(ExceptionContext context)
    {
        /*var errorResponse = new ResponseErrorJson(ResourceErrorMessages.);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);*/
    }
}