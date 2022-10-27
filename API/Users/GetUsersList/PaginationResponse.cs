using Application;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;

namespace API.Users.GetUsersList;

public class PaginationResponse
{
    public HttpRequest Request { get; }
    public Pagination Pagination { get; }
    public int Count { get; }

    public string Location => $"{Request.Scheme}://{Request.Host}{Request.Path}";
    public string Href => $"{Request.Path}";
    public string Self => Request.GetDisplayUrl();
    public int CurrentPage => Pagination.Page;

    public string? PreviousPageQueryParams()
    {
        var queryString = System.Web.HttpUtility.ParseQueryString(Request.QueryString.ToString());
        queryString.Remove("page");
        queryString.Add("page", (CurrentPage - 1).ToString());

        return "?" + queryString;
    }

    public string? NextPageQueryParams()
    {
        var queryString = System.Web.HttpUtility.ParseQueryString(Request.QueryString.ToString());
        queryString.Remove("page");
        queryString.Add("page", (CurrentPage + 1).ToString());

        return "?" + queryString;
    }
    
    public string? Previous => CurrentPage == 1 ? string.Empty : PreviousPageQueryParams();
    public string? Next => CurrentPage >= Count / Pagination.Size ? string.Empty : NextPageQueryParams(); 

    public PaginationResponse(HttpRequest request, Pagination pagination, int count)
    {
        Request = request;
        Pagination = pagination;
        Count = count;
    }

    public object Links => new
    {
        previous = new
        {
            self = $"{Location}{Previous}",
            href = $"{Href}{Previous}"
        },
        current = new
        {
            self = Self,
            href = $"{Href}{Request.QueryString}"
        },
        next = new
        {
            self = $"{Location}{Next}",
            href = $"{Href}{Next}"
        }
    };
}