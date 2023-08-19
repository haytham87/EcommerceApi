using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepo
    {
        Task<customBasket> GetAllCustomBasket(string id);
        Task<customBasket> UpdateCustomBasket(customBasket customBasketItems);
        Task<bool> DeleteCustomBasket(string id);

    }
}
