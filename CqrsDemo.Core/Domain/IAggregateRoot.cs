namespace CqrsDemo.Core.Domain
{
    /// <summary>
    /// Identifies an aggregate root within a domain
    /// An aggregate refers to a cluster of domain objects grouped together to match transactional consistency. Those objects could be instances of entities.
    /// Transactional consistency means that an aggregate is guaranteed to be consistent and up to date at the end of a business action. 
    /// Example: Order=>Positions
    /// </summary>
    public interface IAggregateRoot { }
}