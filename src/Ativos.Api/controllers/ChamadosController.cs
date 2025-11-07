using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
public class ChamadosController : Controller
{
    //Chamados Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterChamadosJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Route("register")]
    public async Task<IActionResult> RegisterChamados([FromServices] IRegisterChamadosUseCase useCase, [FromBody] RequestChamadosJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}