using AutoMapper;
using Commerceee.DTOS;
using Commerceee.Helper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerceee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepostory;
        private readonly IMapper _mapper;
        public ProductController(IProductRepo productRepostory,IMapper mapper)
        {
            _productRepostory = productRepostory;
            _mapper = mapper;
        }
        //[EnableCors("MyAllowSpecificOrigins")]
        [HttpGet("GetAllProduct/{brandId}/{pageCount}/{pageSize}")]
        public async Task<IReadOnlyList<ProductToReturnDTO>> GetProducts(int? brandId,int pageCount, int pageSize=9)
        {
            var res = await _productRepostory.GetProductsList(brandId);
            var c = res.Skip((pageCount - 1) * pageSize).Take(pageSize).ToList();
            ProductResponse resOfPrd = new ProductResponse
            {
                products = c,
                PageCount=pageCount,
                PageSize=pageSize
             };
            var authorDTOs = _mapper.Map<List<ProductToReturnDTO>>(resOfPrd.products);
            return authorDTOs;
        }
        [HttpGet("GetAllTypes")]
        public async Task<IReadOnlyList<ProductType>> GetTypes()
        {
            var res = await _productRepostory.GetTypesList();
            
            return res;
        }
        [HttpGet("GetAllBrands")]
        public async Task<IReadOnlyList<BrandType>> GetBrands()
        {
            var res = await _productRepostory.GetBrandsList();

            return res;
        }
      

        [HttpGet("GetProductById/{id}")]
        public async Task<ProductToReturnDTO> GetProductById(int id)
        {
            var res = await _productRepostory.GetProduct(id);
            return _mapper.Map<Product,ProductToReturnDTO>(res) ;
        }
    }
}