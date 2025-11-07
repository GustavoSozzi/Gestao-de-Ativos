using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Application.UseCases.Register.Contratos;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

public class ContratoController : Controller
{
    //Contratos Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterContratosJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Route("register")]
    public async Task<IActionResult> RegisterContratos([FromServices] IRegisterContratosUseCase useCase, [FromBody] RequestContratosJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}