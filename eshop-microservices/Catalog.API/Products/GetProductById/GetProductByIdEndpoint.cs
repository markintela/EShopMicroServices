
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductsByIdResponse(Product products);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                return  Results.Ok(result);  

            }).WithName("Get Product by Id")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Get Product by Id");
        }
    }
}
