using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ordrAggragate
{
    public class productItemOrder
    {
        public int productItemId { get; set; }
        public string productName { get; set; }
        public string Pictureurl  { get; set; }

        public productItemOrder()
        {

        }
        public productItemOrder(int productId,string productname,string pictureurl)
        {
            productItemId = productId;
            productName=productname;
            Pictureurl = pictureurl;

        }

    }
}
