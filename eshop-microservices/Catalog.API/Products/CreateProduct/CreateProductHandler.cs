
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, string ImgFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler (IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //create product entity from command object
            //save database
            //return CreateProductResult

            var product = new Product { 
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImageFile = command.ImgFile,
                Price = command.Price
                       
            };


            //save to database

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            var result = new CreateProductResult(product.Id);

            return result;
        }
    }
}
