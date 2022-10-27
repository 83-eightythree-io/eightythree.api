using Application;
using Application.Users.SignUp;
using Business.Users;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Users.SignUp;

[ApiController]
public class UsersSignUpController : ApiController
{
    private readonly IService<SignUpCommand, User> _service;

    public UsersSignUpController(IService<SignUpCommand, User> service)
    {
        _service = service;
    }

    [HttpPost, Route("/users")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute([FromBody] SignUpCommand command)
    {
        try
        {
            var user = _service.Execute(command);

            return Created(
                $"{Location}/{user.Id}", 
                new
                {
                    id = user.Id,
                    links = new
                    {
                        self = $"{Location}/{user.Id}",
                        href = $"{Path}/{user.Id}",
                        rel = $"{Path}"
                    }
                });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}