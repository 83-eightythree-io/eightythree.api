using Application;
using Application.Organizations.UpdateOrganization;
using Business.Organizations;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Organizations.UpdateOrganization;

[ApiController]
public class UpdateOrganizationController : ApiController
{
    private readonly IService<UpdateOrganizationCommand, Organization> _service;

    public UpdateOrganizationController(IService<UpdateOrganizationCommand, Organization> service)
    {
        _service = service;
    }

    [Authorize(Roles = Role.Standard)]
    [HttpPatch, Route("/organizations/{id:guid}")]
    [Produces("application/json")]
    [OpenApiTag("Organizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute(Guid id, [FromBody] UpdateOrganizationRequest request)
    {
        try
        {
            var organization = _service.Execute(new UpdateOrganizationCommand(GetUserEmail(), id, request.Name, request.Account));

            return Ok(new
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}