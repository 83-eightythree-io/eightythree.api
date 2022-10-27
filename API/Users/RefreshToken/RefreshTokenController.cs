using Application;
using Application.Users.RefreshToken;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ApplicationException = Application.ApplicationException;

namespace API.Users.RefreshToken;

[ApiController]
public class RefreshTokenController : ApiController
{
    private readonly IService<RefreshTokenCommand, RefreshTokenResult> _service;

    public RefreshTokenController(IService<RefreshTokenCommand, RefreshTokenResult> service)
    {
        _service = service;
    }

    [HttpPost, Route("/users/refresh-token")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post()
    {
        try
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null)
                return Unauthorized(new Error("Refresh token is invalid"));

            var result = _service.Execute(new RefreshTokenCommand(refreshToken));
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = result.RefreshToken.ExpiresAt,
            };

            Response.Cookies.Append(Business.RefreshTokens.RefreshToken.Key, result.RefreshToken.Token, cookieOptions);

            return Ok(new
            {
                token = result.Token
            });
        }
        catch (ApplicationException e)
        {
            return Unauthorized(new Error(e.Message));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}