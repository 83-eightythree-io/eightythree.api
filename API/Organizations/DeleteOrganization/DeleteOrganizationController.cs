using System.Security.Claims;
using Application;
using Application.Organizations.DeleteOrganization;
using Business.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Organizations.DeleteOrganization;

[ApiController]
public class DeleteOrganizationController : ApiController
{
    private readonly IService<DeleteOrganizationCommand, bool> _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteOrganizationController(IService<DeleteOrganizationCommand, bool> service, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = Role.Standard, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete, Route("/organizations/{id:guid}")]
    [Produces("applications/json")]
    [OpenApiTag("Organizations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute(Guid id)
    {
        try
        {
            var result = _service.Execute(new DeleteOrganizationCommand(GetUserEmail(), id));
            if (!result)
                return BadRequest(new Error("The organization cannot be deleted"));
            
            return NoContent();
        }
        catch (UserNotAuthorizedException exception)
        {
            return Unauthorized(new Error(exception.Message));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}