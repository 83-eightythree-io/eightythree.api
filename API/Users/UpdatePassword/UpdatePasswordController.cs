using Application;
using Application.Users.UpdatePassword;
using Application.Users.UpdatePassword.Exceptions;
using Business;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ApplicationException = Application.ApplicationException;

namespace API.Users.UpdatePassword;

public class UpdatePasswordController : ApiController
{
    private readonly IService<UpdatePasswordCommand, bool> _service;

    public UpdatePasswordController(IService<UpdatePasswordCommand, bool> service)
    {
        _service = service;
    }

    [Authorize(Roles = Role.Standard)]
    [HttpPatch, Route("/users/{id:guid}")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Patch(Guid id, [FromBody] UpdatePasswordRequest request)
    {
        try
        {
            _service.Execute(new UpdatePasswordCommand(id, request.OldPassword, request.NewPassword));
            return NoContent();
        }
        catch (UserNotFoundException e)
        {
            return NotFound();
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