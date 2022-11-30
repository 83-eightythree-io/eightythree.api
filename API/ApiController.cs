using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class ApiController : Controller
{
    protected string Location => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
    protected string Scheme => HttpContext.Request.Scheme;
    protected string Host => $"{HttpContext.Request.Host}";
    protected string Path => HttpContext.Request.Path;

    protected string GetUserEmail()
    {
        var userEmail = HttpContext.User.Claims.SingleOrDefault(u => u.Type.Equals(ClaimTypes.Email))?.Value;
        if (userEmail is null)
            throw new UserNotAuthorizedException("User is not authorized");

        return userEmail;
    }
}