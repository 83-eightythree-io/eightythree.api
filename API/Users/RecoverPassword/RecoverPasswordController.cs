using Application;
using Application.Users.RecoverPassword;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Users.RecoverPassword;

[ApiController]
public class RecoverPasswordController : ApiController
{
    private readonly IService<RecoverPasswordCommand, string> _service;

    public RecoverPasswordController(IService<RecoverPasswordCommand, string> service)
    {
        _service = service;
    }

    [HttpPost, Route("/users/password-resets")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] RecoverPasswordCommand command)
    {
        try
        {
            var message = _service.Execute(command);
            return Accepted(new
            {
                message
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}