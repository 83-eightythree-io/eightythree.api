using System.Security.Claims;
using Application;
using Application.Organizations.CreateOrganization;
using Business.Organizations;
using Business.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Organizations.CreateOrganization;

[ApiController]
public class CreateOrganizationController : ApiController
{
    private readonly IService<CreateOrganizationCommand,Organization> _service;
    private readonly IHttpContextAccessor _httpContext;

    public CreateOrganizationController(IService<CreateOrganizationCommand, Organization> service, IHttpContextAccessor httpContext)
    {
        _service = service;
        _httpContext = httpContext;
    }
    
    [Authorize(Roles = Role.Standard, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost, Route("/organizations")]
    [Produces("application/json")]
    [OpenApiTag("Organizations")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute([FromBody] CreateOrganizationRequest request)
    {
        try
        {
            var organization = _service.Execute(new CreateOrganizationCommand(GetUserEmail(), request.Name, request.Account, request.TermsAndConditionsAccepted));

            return Created(
                $"{Location}/{organization.Id}",
                new
                {
                    id = organization.Id,
                    links = new
                    {
                        self = $"{Location}/{organization.Id}",
                        href = $"{Path}/{organization.Id}",
                        rel = $"{Path}"
                    }
                });
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