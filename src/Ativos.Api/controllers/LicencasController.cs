using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
public class LicencasController : Controller
{
    //Licencas Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterLicencasJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Route("register")]
    public async Task<IActionResult> RegisterLicencas([FromServices] IRegisterLicencasUseCase useCase, [FromBody] RequestLicencasJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}