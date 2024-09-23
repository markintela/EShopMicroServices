﻿

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQuery() : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> products);

    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            logger.LogInformation("GetProductsQueryHandler -> {@query}");

            var products =  await session.Query<Product>().ToListAsync(cancellationToken);

            var result = new GetProductsResult(products);

            return result;
        }
    }
}
