namespace Application;

public interface IQuery<in TQuery, out TResult>
{
    TResult Execute(TQuery query);
}