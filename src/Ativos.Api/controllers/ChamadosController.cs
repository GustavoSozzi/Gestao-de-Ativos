using Ativos.Application.UseCases.GetAll.Chamados;
using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Application.UseCases.Update.Chamados;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChamadosController : ControllerBase
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
    
    //GetAllAtivos
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterChamadosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllChamados([FromServices] IGetAllChamadosUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Chamados.Count != 0)
            return Ok(response);

        return NoContent();
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateChamadosUseCase useCase, [FromRoute] long id,
        [FromBody] RequestChamadosJson request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }
}