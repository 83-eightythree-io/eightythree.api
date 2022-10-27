using Application;
using Application.Users.ResetPassword;
using Business;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ApplicationException = Application.ApplicationException;

namespace API.Users.ResetPassword;

public class ResetPasswordController : ApiController
{
    private readonly IService<ResetPasswordCommand, bool> _service;

    public ResetPasswordController(IService<ResetPasswordCommand, bool> service)
    {
        _service = service;
    }

    [HttpPatch, Route("/users/passwords")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Patch([FromBody] ResetPasswordCommand command)
    {
        try
        {
            _service.Execute(command);
            return NoContent();
        }
        catch (BusinessException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(new
            {
                error = e.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}