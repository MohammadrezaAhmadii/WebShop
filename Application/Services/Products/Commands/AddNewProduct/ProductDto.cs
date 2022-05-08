using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Services.Products.Commands
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; }
        public bool Displayed { get; set; }
        public List<ProductFeaturesDto> ProductFeatures { get; set; }
        public List<IFormFile> ProductImages { get; set; }
    }
}
