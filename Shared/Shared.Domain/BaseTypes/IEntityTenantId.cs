namespace Shared.Domain.BaseTypes
{
    public interface IEntityTenantId
    {
        public string TenantId { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime? UpdatedDateUTC { get; set; }

        public bool IsDeleted { get; set; }
    }
}
