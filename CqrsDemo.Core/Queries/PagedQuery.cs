namespace CqrsDemo.Core.Queries
{
    public abstract class PagedQuery
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}