namespace Application;

public class Sorting
{
    public string Sort { get; }
    public string Order { get; }

    public Sorting(string sort, string order = "asc")
    {
        Sort = sort;
        Order = order;
    }
}