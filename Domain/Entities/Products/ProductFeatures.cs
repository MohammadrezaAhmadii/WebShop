using Domain.Entities.Common;

namespace Domain.Entities.Products
{
    public class ProductFeatures:BaseEntity
    {
        public long ProductId { get; set; } 
        public virtual Product Product { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
