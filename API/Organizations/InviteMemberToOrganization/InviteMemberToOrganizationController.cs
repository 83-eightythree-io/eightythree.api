using Application;
using Application.Organizations.InviteMemberToOrganization;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Organizations.InviteMemberToOrganization;

[ApiController]
public class InviteMemberToOrganizationController : ApiController
{
    private readonly IService<InviteMemberToOrganizationCommand, bool> _service;

    public InviteMemberToOrganizationController(IService<InviteMemberToOrganizationCommand, bool> service)
    {
        _service = service;
    }

    [Authorize(Roles = Role.Standard)]
    [HttpPatch, Route("/organizations/{id:guid}")]
    [Produces("application/json")]
    [OpenApiTag("Organizations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute(Guid id, [FromBody] InviteMemberToOrganizationCommand command)
    {
        try
        {
            var result = _service.Execute(new InviteMemberToOrganizationCommand());
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}