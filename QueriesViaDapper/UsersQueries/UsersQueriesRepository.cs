using System.Data;
using Application;
using Application.Users.GetUser;
using Application.Users.GetUsersList;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QueriesViaDapper.UsersQueries;

public class UsersQueriesRepository : 
                    IQuery<GetUserQuery, UserResult>,
                    IQuery<GetUsersListQuery, GetUsersListResult>
{
    private readonly SqlConnection _connection;

    public UsersQueriesRepository(string connection)
    {
        _connection = new SqlConnection(connection);
    }

    public UserResult Execute(GetUserQuery query)
    {
        _connection.Open();
        const string command = "select Id, Name, Email, CreatedAt from Users where Id = @id";
        var user = _connection.QueryFirstOrDefault<UserResult>(command, new { @id = query.UserId });
        
        _connection.Close();
        return user;
    }

    public GetUsersListResult Execute(GetUsersListQuery query)
    {
        _connection.Open();
        var command = "select " +
                      "Id, Name, Email, CreatedAt, Deleted, DeletedAt " +
                      "from Users where 1=1 ";
        
        var counterCommand = "select " +
                      "count(*) " +
                      "from Users where 1=1 ";

        command += PrepareQuery(query, true);
        counterCommand += PrepareQuery(query, false);

        var counter = _connection.QueryFirstOrDefault<int>(counterCommand, GetParams(query));
        var users = _connection.Query<GetUserResult>(command, GetParams(query));

        var result = new GetUsersListResult
        {
            Count = counter,
            Users = users
        };
        
        _connection.Close();
        
        return result;
    }

    private static string PrepareQuery(GetUsersListQuery query, bool pagination)
    {
        var command = "";
        
        if (query.Deleted)
        {
            command += "and Deleted = 1 ";
        }
        else
        {
            command += "and Deleted = 0 ";
        }

        if (!string.IsNullOrEmpty(query.Search))
        {
            command += $"and (Email like '%{query.Search}%' or Name like '%{query.Search}%') ";
        } 
        else if (!string.IsNullOrEmpty(query.Email))
        {
            command += "and Email = @email ";
        }
        else if (!string.IsNullOrEmpty(query.Name))
        {
            command += "and Name = @name ";
        }

        if (!string.IsNullOrEmpty(query.Sorting.Sort))
            command += "order by @sort @order ";

        if (!pagination) return command;
        
        if (!string.IsNullOrEmpty(query.Sorting.Sort))
            command += "offset @offset rows fetch next @size rows only";
        else
            command += "order by Id asc offset @offset rows fetch next @size rows only";

        return command;
    }

    private static object GetParams(GetUsersListQuery query)
    {
        return new
        {
            @email = query.Email,
            @name = query.Name,
            @sort = query.Sorting.Sort,
            @order = query.Sorting.Order,
            @offset = query.Pagination.Offset,
            @size = query.Pagination.Size
        };
    }
}