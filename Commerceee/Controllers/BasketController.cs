using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerceee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketRepo _IBasketRepo;
        public BasketController(IBasketRepo iBasketRepo)
        {
            _IBasketRepo = iBasketRepo;
        }
        [HttpGet]
        public async Task<ActionResult<customBasket>> GetcustomBasketByID(string id) {
            var data = await _IBasketRepo.GetAllCustomBasket(id);
            return Ok(data ?? new customBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<customBasket>> updatecustomBasket(customBasket basket)
        {
            var data=_IBasketRepo.UpdateCustomBasket(basket);
            return Ok(data);
        }
        [HttpDelete]
        public async Task deleteCustomBasket(string id)
        {
            await _IBasketRepo.DeleteCustomBasket(id);
        }
    }
}
