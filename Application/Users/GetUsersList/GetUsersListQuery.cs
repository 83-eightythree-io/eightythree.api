namespace Application.Users.GetUsersList;

public class GetUsersListQuery
{
    public string Search { get; }
    public bool Deleted { get; }
    public string Email { get; }
    public string Name { get; }
    public Pagination Pagination { get; }
    public Sorting Sorting { get; }

    public GetUsersListQuery(string search, bool deleted, string email, string name, Pagination pagination, Sorting sorting)
    {
        Search = search;
        Deleted = deleted;
        Email = email;
        Name = name;
        Pagination = pagination;
        Sorting = sorting;
    }
}