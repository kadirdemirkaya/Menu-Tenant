﻿namespace Shared.Domain.BaseTypes
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntityTenantId
        where TId : notnull
    {
        public TId Id { get; protected set; }
        public string TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime? UpdatedDateUTC { get; set; }


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

        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public void SetCreatedDateUTC(DateTime createdDateUTC)
        {
            CreatedDateUTC = createdDateUTC;
        }

        public void SetUpdatedDateUTC(DateTime updatedDateUTC)
        {
            UpdatedDateUTC = updatedDateUTC;
        }

        public void SetTenantId(string tenantId)
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
