using Ativos.Application.UseCases.GetAll;
using Ativos.Application.UseCases.Register.Localizacao;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocalizacaoController : ControllerBase
{
    //Localizacao Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterLocalizacaoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterLocalizacao([FromServices] IRegisterLocalizacaoUseCase useCase, [FromBody] RequestLocalizacaoJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
    
    //GetAllLocalizacao
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterLocalizacaoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllLocalizacao([FromServices] IGetAllLocalizacaoUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Localizacoes.Count != 0)
            return Ok(response);

        return NoContent();
    }
}