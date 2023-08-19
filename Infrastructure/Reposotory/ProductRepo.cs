using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposotory
{
    public class ProductRepo : IProductRepo
    {
        private readonly CommerceContext _context;
        public ProductRepo(CommerceContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<BrandType>> GetBrandsList()
        {
            return await _context.BrandTypes.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.Include(x => x.BrandType).Include(x => x.ProductType).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsList(int? brandId)
        {
            if(brandId!=0)
             return await _context.Products.Include(x => x.BrandType).Include(x => x.ProductType).Where(x => x.BrandId== brandId).ToListAsync();
            return await _context.Products.Include(x => x.BrandType).Include(x => x.ProductType).ToListAsync();

        }

        public async Task<IReadOnlyList<ProductType>> GetTypesList()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
