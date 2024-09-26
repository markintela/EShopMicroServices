
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImgFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler -> {@queryRequest}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null) {

                throw new ProductNotFoundException();        
            }

            var productName = product.Name;
            var productDescription = product.Description;
            var productCategory = product.Category;
            var imgFile = product.ImageFile;
            var productPrice = product.Price;


            session.Update(product);
            await session.SaveChangesAsync();


            return new UpdateProductResult(true);
        }
    }
}
