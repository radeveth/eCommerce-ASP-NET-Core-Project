namespace eCommerce_RESTful_API.Controllers
{
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Categories;
    using eCommerceAPI.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EcommerceApiDbContext dbContext;
        private readonly ILogger<ProductsController> logger;

        public CategoriesController(ILogger<ProductsController> logger, EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryViewModel>> GetByIdAync(int id)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetByIdAsync"));

            Category category = await this.dbContext
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            return this.mapper.Map<CategoryViewModel>(category);
        }

        [HttpGet("all")]
        public IEnumerable<CategoryViewModel> GetAll()
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetAll"));

            IEnumerable<Category> categories = this.dbContext.Categories;

            return this.mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync(CategoryFoemModel categoryFoem)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "CreateAsync"));

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

        private static string LogRequestInformation(string method, string actionName)
        {
            return $"Method: {method}; Controller: CategoriesController; Action: {actionName};";
        }
    }
}
