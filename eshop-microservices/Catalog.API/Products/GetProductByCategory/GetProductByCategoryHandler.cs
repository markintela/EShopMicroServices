
using Catalog.API.Products.GetProductById;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string categoryName) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> products);
    internal class GetProductByCategoryHandler (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler -> {@query}", query);

            var products = await session.Query<Product>().Where(c => c.Category.Contains(query.categoryName)).ToListAsync();

            if (products == null || products.Count == 0)
            {
                throw new ProductNotFoundException();
            }

            var result = new GetProductsByCategoryResult(products);

            return result;
        }
    }
}
