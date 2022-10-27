namespace Application;

public class Pagination
{
    public int Page { get; }
    public int Size { get; }

    public int Offset => (Page - 1) * Size;

    public Pagination(int page = 1, int size = 20)
    {
        Page = page;
        Size = size;
    }
}