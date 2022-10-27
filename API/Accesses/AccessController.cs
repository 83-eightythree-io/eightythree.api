using Application;
using Application.Accesses;
using Application.Accesses.Exceptions;
using Business;
using Business.RefreshTokens;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Accesses;

[ApiController]
public class AccessController : ApiController
{
    private readonly IService<UserAccessCommand, UserAccessResult> _service;

    public AccessController(IService<UserAccessCommand, UserAccessResult> service)
    {
        _service = service;
    }

    [HttpPost, Route("/users/accesses")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post(UserAccessCommand command)
    {
        try
        {
            var tokens = _service.Execute(command);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokens.RefreshToken.ExpiresAt,
            };
            
            Response.Cookies.Append(RefreshToken.Key, tokens.RefreshToken.Token, cookieOptions);
            
            return Ok(new
            {
                token = tokens.AccessToken
            });
        }
        catch (BusinessException e)
        {
            return BadRequest(new Error(e.Message));
        }
        catch (InvalidUserCredentialsException e)
        {
            return Unauthorized(new Error(e.Message));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}