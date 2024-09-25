
using Catalog.API.Exceptions;
using Catalog.API.Products.GetProducts;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

    public record GetProductsByIdResult(Product products);

    internal class GetProductByIdQueryHandler (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger): IQueryHandler<GetProductByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductByIdQuery queryRequest, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler -> {@queryRequest}", queryRequest);

            var product = await session.LoadAsync<Product>(queryRequest.Id, cancellationToken);

            if (product == null) {
                throw new ProductNotFoundException();
            }

            var result = new GetProductsByIdResult(product);

            return result; 
        }
    }
}
