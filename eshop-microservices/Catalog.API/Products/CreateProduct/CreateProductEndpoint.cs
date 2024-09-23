using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImgFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result =  await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                var route = Results.Created($"/products/{ response.Id}", response);

                return route;
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
 
        }
    }
}
