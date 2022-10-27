using Application;
using Application.Users.GetUsersList;
using Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.Users.GetUsersList;

public class GetUsersListController : ApiController
{
    private readonly IQuery<GetUsersListQuery, GetUsersListResult> _query;

    public GetUsersListController(IQuery<GetUsersListQuery, GetUsersListResult> query)
    {
        _query = query;
    }

    [Authorize(Roles = Role.Admin)]
    [HttpGet, Route("/users")]
    [Produces("application/json")]
    [OpenApiTag("Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery] UsersListParameters parameters)
    {
        try
        {
            var pagination = new Pagination(parameters.Page == 0 ? 1 : parameters.Page,
                parameters.Size == 0 ? 20 : parameters.Size);
            
            var result = _query.Execute(
                new GetUsersListQuery(
                    parameters.Search,
                    parameters.Deleted,
                    parameters.Email,
                    parameters.Name,
                    pagination,
                    new Sorting(parameters.Sort, parameters.Order)
                ));

            var users = result.Users.Select(u => new
            {
                user = u,
                links = new
                {
                    self = $"{Location}/{u.Id}",
                    rel = "users",
                    href = $"{Path}/{u.Id}"
                }
            });

            var paginationResponse = new PaginationResponse(HttpContext.Request, pagination, result.Count);
            
            return Ok(new
            {
                count = result.Count,
                links = paginationResponse.Links,
                users
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}