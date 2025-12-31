using Ativos.Application.UseCases.Delete.Ativos;
using Ativos.Application.UseCases.GetAll;
using Ativos.Application.UseCases.GetById;
using Microsoft.AspNetCore.Mvc;
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Update;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Authorization;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AtivosController : ControllerBase
{
    //Ativos Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterAtivosJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAtivos([FromServices] IRegisterAtivosUseCase useCase, [FromBody] RequestAtivosJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
    
    
    //GetAllAtivos
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterAtivosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAtivos(
        [FromServices] IGetAllAtivosUseCase useCase,
        [FromQuery] string? nome = null,
        [FromQuery] string? modelo = null,
        [FromQuery] string? tipo = null,
        [FromQuery] long? codInventario = null,
        [FromQuery] string? cidade = null,
        [FromQuery] string? estado = null,
        [FromQuery] long? matriculaUsuario = null,
        [FromQuery] string? nomeUsuario = null)
    {
        var response = await useCase.Execute(nome, modelo, tipo, codInventario, cidade, estado, matriculaUsuario, nomeUsuario);
        if (response.Ativos.Count != 0)
            return Ok(response);

        return NoContent();
    }
    
    [HttpGet] //Get ativos by id
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseAtivoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetAtivoByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteAtivoUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateAtivosUseCase useCase, [FromRoute] long id,
        [FromBody] RequestAtivosJson request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }
    
}