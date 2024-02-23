using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductQuery : IRequest<Pagination<ProductResponse>>
    {
      
        public CatalogSpecParams CatalogSpecParams { get; set; }

        public GetAllProductQuery(CatalogSpecParams catalogSpecParams)
        {
            CatalogSpecParams = catalogSpecParams;
        }
    }
}
