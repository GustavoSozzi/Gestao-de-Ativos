using Ativos.Application.UseCases.GetAll.Usuarios;
using Ativos.Application.UseCases.GetById;
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Application.UseCases.Update.Usuarios;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Ativos.Communication.responses.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsuariosController : ControllerBase

{
    //Usuarios Controller
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUsuariosJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Route("register")]
    public async Task<IActionResult> RegisterUsuarios([FromServices] IRegisterUsuariosUseCase useCase, [FromBody] RequestUsuariosJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
    
    //GetAll Usuarios
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterUsuariosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllUsuarios(
        [FromServices] IGetAllUsuarioUseCase useCase, //query params
        [FromQuery] long? matricula = null,
        [FromQuery] string? nome = null,
        [FromQuery] string? departamento = null,
        [FromQuery] string? cargo = null,
        [FromQuery] string? role = null)
    {
        var response = await useCase.Execute(matricula, nome, departamento, cargo, role);
        if (response.Usuarios.Count != 0)
            return Ok(response);

        return NoContent();
    }
    
    [HttpGet] //Get user by id
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseUsuarioJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetUsuarioByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateUsuariosUseCase useCase, [FromRoute] long id,
        [FromBody] RequestUsuariosJson request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }
}