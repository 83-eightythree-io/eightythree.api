namespace Application;

public interface IService<in TCommand, out TReturn>
{
    TReturn Execute(TCommand command);
}