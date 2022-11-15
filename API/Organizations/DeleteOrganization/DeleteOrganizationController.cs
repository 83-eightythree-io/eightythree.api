using Application;
using Application.Organizations.DeleteOrganization;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Organizations.DeleteOrganization;

[ApiController]
public class DeleteOrganizationController : ApiController
{
    private readonly IService<DeleteOrganizationCommand, bool> _service;

    public DeleteOrganizationController(IService<DeleteOrganizationCommand, bool> service)
    {
        _service = service;
    }

    [Authorize(Roles = Role.Standard)]
    [HttpDelete, Route("/organizations/{id:guid}")]
    [Produces("applications/json")]
    [OpenApiTag("Organizations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Execute(Guid id)
    {
        try
        {
            var result = _service.Execute(new DeleteOrganizationCommand(id));
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}