namespace Shared.Domain.BaseTypes
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : notnull
    {
        public string TenantId { get; protected set; }


        public AggregateRoot()
        {
    
        }
        protected AggregateRoot(TId id) : base(id)
        {

        }
    }
}
