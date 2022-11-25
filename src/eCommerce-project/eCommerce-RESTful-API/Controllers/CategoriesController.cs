namespace eCommerce_RESTful_API.Controllers
{
    using AutoMapper;
    using eCommerce.Data;
    using eCommerce.Data.Models;
    using eCommerce.InputModels.Categories;
    using eCommerce.Services.Data.CategoriesServices;
    using eCommerce.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly ICategoryService categoryService;

        public CategoriesController(ILogger<ProductsController> logger, ICategoryService categoryService)
        {
            this.logger = logger;
            this.categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetByIdAync(int id)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetByIdAsync"));

            return await this.categoryService.GetByIdAync(id);
        }

        [HttpGet]
        public IEnumerable<CategoryViewModel> GetAll()
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetAll"));

            return this.categoryService.GetAll();
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync(CategoryFoemModel categoryForm)
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
                await this.categoryService.CreateAsync(categoryForm);
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
