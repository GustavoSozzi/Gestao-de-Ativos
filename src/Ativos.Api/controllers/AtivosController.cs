using Ativos.Application.UseCases.Delete.Ativos;
using Ativos.Application.UseCases.GetAll;
using Microsoft.AspNetCore.Mvc;
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Update;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
public class AtivosController : Controller
{
    //Ativos Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterAtivosJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Route("register")]
    public async Task<IActionResult> RegisterAtivos([FromServices] IRegisterAtivosUseCase useCase, [FromBody] RequestAtivosJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
    
    
    //GetAllAtivos
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterAtivosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAtivos([FromServices] IGetAllAtivosUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Ativos.Count != 0)
            return Ok(response);

        return NoContent();
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