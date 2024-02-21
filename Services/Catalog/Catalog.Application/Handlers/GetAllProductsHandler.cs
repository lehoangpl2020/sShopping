using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProducts();

            return productList.Select(x => new ProductResponse
            {
                Brands = x.Brands,
                Description = x.Description,
                Id = x.Id,
                ImageFile = x.ImageFile,
                Name = x.Name,
                Price = x.Price,
                Summary = x.Summary,
                Types = x.Types

            }).ToList();
        }
    }
}
