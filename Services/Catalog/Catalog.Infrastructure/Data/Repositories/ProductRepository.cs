using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
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

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }



        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
                .Brands
                .Find(b => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                .Types
                .Find(t => true)
                .ToListAsync();
        }
    }
}
