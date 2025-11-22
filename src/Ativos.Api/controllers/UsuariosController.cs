using Ativos.Application.UseCases.GetAll.Usuarios;
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
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
        [FromServices] IGetAllUsuarioUseCase useCase,
        [FromQuery] long? matricula = null)
    {
        var response = await useCase.Execute(matricula);
        if (response.Usuarios.Count != 0)
            return Ok(response);

        return NoContent();
    }
}