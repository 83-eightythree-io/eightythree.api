using Application;
using Application.Users.DeleteUser;
using Application.Users.DeleteUser.Exceptions;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Users.DeleteUser;

public class DeleteUserController : ApiController
{
    private readonly IService<DeleteUserCommand, bool> _service;

    public DeleteUserController(IService<DeleteUserCommand, bool> service)
    {
        _service = service;
    }

    [Authorize(Roles = Role.Standard)]
    [HttpDelete, Route("/users/{id:guid}")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var result = _service.Execute(new DeleteUserCommand(id));
            return NoContent();
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}