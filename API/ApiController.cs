using Microsoft.AspNetCore.Mvc;

namespace API;

public class ApiController : Controller
{
    protected string Location => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
    protected string Scheme => HttpContext.Request.Scheme;
    protected string Host => $"{HttpContext.Request.Host}";
    protected string Path => HttpContext.Request.Path;
}