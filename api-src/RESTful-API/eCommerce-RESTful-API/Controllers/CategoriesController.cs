namespace eCommerce_RESTful_API.Controllers
{
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly EcommerceApiDbContext dbContext;
        private readonly IMapper mapper;

        public CategoriesController(ILogger<ProductsController> logger, EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this._logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync(CategoryFoemModel categoryFoem)
        {
            if (!this.ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = this.ModelState
                    .Values
                    .SelectMany(modelState => modelState.Errors)
                    .Select(error => error.ErrorMessage);

                return new JsonResult(errorMessages);
            }

            try
            {
                Category category = new Category()
                {
                    Name = categoryFoem.Name,
                    Description = categoryFoem.Description,
                    UserId = categoryFoem.UserId,
                };

                await this.dbContext.Categories.AddAsync(category);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(this.Ok("Category successfully created."));
        }
    }
}
