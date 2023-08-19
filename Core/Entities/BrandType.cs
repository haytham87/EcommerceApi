using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class BrandType
    {
        public int Id { get; set; }

        public string? BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
