using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ordrAggragate
{
    public class DeliveryMethod
    {
        public int Id { get; set; }
        public string shortName { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
