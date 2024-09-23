
namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQuery() : IQuery<GetProductsByIdResponse>;

    public record GetProductsByIdResponse(Product products);
    public class GetProductByIdHandler : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid Id, ISender sender) =>
            {

            });
   
        }
    }
}
