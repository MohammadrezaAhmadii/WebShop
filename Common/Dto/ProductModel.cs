using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ProductModel
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
        public DateTime? UpdateDateTime { get; set; }
        public List<ProductFeaturesModel> ProductFeatures { get; set; }
        public List<IFormFile> ProductImages { get; set; }
    }

    public class ProductFeaturesModel
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
