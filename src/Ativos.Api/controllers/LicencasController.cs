using Ativos.Application.UseCases.GetAll.Licencas;
using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] //apenas se estiver logado acessa as funcoes da API
public class LicencasController : ControllerBase
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
    
    //GetAllLicencas
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterLicencasJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllLicencas([FromServices] IGetAllLicencasUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Licencas.Count != 0)
            return Ok(response);

        return NoContent();
    }
}