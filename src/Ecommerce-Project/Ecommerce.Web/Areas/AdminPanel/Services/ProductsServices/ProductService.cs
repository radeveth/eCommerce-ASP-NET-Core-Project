namespace Ecommerce.Web.Areas.AdminPanel.Services.ProductsServices
{
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Data;

    public class ProductService : BaseService, IProductService
    {
        private readonly EcommerceDbContext dbContext;

        public ProductService(EcommerceDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        // Update
        public async Task Update(int id, ProductFormModel productForm)
        {
            Product product = this.GetById(id);

            product.Name = productForm.Name;
            product.Quantity = productForm.Quantity;
            product.DiscountPercentage = productForm.DiscountPercentage;
            product.BrandId = productForm.BrandId;
            product.CategoryId = productForm.CategoryId;
            product.Description = productForm.Description;
            product.Price = productForm.Price;
            product.Status = productForm.Status;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task SetDiscountToProduct(int id, decimal discount)
        {
            Product product = this.GetUnDeletedProducts().FirstOrDefault(p => p.Id == id);

            product.DiscountPercentage = discount;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateDiscountStatusOfProduct(int id, bool discountStatus)
        {
            Product product = this.GetUnDeletedProducts().FirstOrDefault(p => p.Id == id);

            product.IsHaveDiscount = discountStatus;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateQuantityOfProduct(int id, int quantity)
        {
            Product product = this.GetById(id);
            product.Quantity = quantity;

            if (product.Quantity == 0)
            {
                product.Status = Status.Unavailable;
            }
            else if (product.Quantity != 0)
            {
                product.Status = Status.Available;
            }

            await this.dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            Product product = this.GetById(id);

            product.DeletedOn = DateTime.Now;
            product.IsDeleted = true;

            await this.dbContext.SaveChangesAsync();
        }

        // Usefull Methods
        private Product GetById(int id)
        {
            return this
                .GetUnDeletedProducts()
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
