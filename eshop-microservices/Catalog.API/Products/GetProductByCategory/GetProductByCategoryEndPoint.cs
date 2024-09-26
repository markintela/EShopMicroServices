

namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryResponse(IEnumerable<Product> products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryName}", 
            async (string categoryName, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(categoryName));

                return Results.Ok(result);

            }).WithName("Get Product by Category")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Category")
            .WithDescription("Get Product by Category");
        }
    }
}
