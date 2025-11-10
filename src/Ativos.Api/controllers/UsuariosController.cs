using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : Controller
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
}