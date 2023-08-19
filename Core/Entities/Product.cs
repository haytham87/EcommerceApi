using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Price { get; set; }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public virtual BrandType? BrandType { get; set; }

        public virtual ProductType? ProductType { get; set; }
    }

}
