using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class customBasket
    {
        public customBasket()
        {

        }
        public customBasket(string id)
        {
            this.Id=id;
        }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }=new List<BasketItem>() { };
    }
}
