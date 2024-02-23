using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Catalog.Core.Specs;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(specParams.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(specParams.Search));
                filter &= searchFilter;
            }

            if (!string.IsNullOrEmpty(specParams.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, specParams.BrandId);
                filter &= brandFilter;
            }
            if (!string.IsNullOrEmpty(specParams.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, specParams.TypeId);
                filter &= typeFilter;
            }

            var query = _context
            .Products
            .Find(filter);

            query = specParams.Sort switch
            {
                "priceAsc" => query.Sort(Builders<Product>.Sort.Ascending("Price")),
                "priceDesc" => query.Sort(Builders<Product>.Sort.Descending("Price")),
                _ => query.Sort(Builders<Product>.Sort.Ascending("Name")),
            };

            var data = await query
                .Skip(specParams.PageSize * (specParams.PageIndex - 1))
                .Limit(specParams.PageSize)
                .ToListAsync();

            return new Pagination<Product>
            {
                PageSize = specParams.PageSize,
                PageIndex = specParams.PageIndex,
                Data = data,
                Count = await _context.Products.Find(filter).CountDocumentsAsync()
            };
        }

        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }



        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
