namespace Shared.Domain.BaseTypes
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>> , ITenantId
        where TId : notnull
    {
        public TId Id { get; protected set; }
        public string TenantId { get; set; }
        public bool IsDeleted { get; protected set; }
        public DateTime CreatedDateUTC { get; protected set; }
        public DateTime? UpdatedDateUTC { get; protected set; }
        public Entity()
        {
            IsDeleted = false;
            CreatedDateUTC = DateTime.UtcNow;
            UpdatedDateUTC = null;
        }
        protected Entity(TId id)
        {
            Id = id;
            IsDeleted = false;
            CreatedDateUTC = DateTime.UtcNow;
            UpdatedDateUTC = null;
        }

        protected void SetTenantId(string tenantId)
        {
            TenantId = tenantId;
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> Entity && Id.Equals(Entity.Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
