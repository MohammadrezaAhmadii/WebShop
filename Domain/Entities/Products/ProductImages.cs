using Domain.Entities.Common;

namespace Domain.Entities.Products
{
    public class ProductImages:BaseEntity
    {
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Src { get; set; }
    }
}
