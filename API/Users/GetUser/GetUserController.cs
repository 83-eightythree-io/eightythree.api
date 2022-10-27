using Application;
using Application.Users.GetUser;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Users.GetUser;

public class GetUserController : ApiController
{
    private readonly IQuery<GetUserQuery, UserResult> _query;

    public GetUserController(IQuery<GetUserQuery, UserResult> query)
    {
        _query = query;
    }

    [Authorize(Roles = $"{Role.Standard}, {Role.Admin}")]
    [HttpGet, Route("/users/{id:guid}")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get(Guid id)
    {
        try
        {
            var user = _query.Execute(new GetUserQuery(id));
            return Ok(new
            {
                user,
                links = new
                {
                    self = $"{Location}",
                    rel = "users",
                    href = $"{Path}"
                }
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}