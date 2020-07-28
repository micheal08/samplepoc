using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            return await _storeContext.Products
                .Include(x => x.ProductType)
                .Include(y => y.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products
                .Include(x => x.ProductType)
                .Include(y => y.ProductBrand)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
    }
}
