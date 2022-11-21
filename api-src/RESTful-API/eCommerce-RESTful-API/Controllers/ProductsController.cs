namespace eCommerce_RESTful_API.Controllers
{
    using System.Text;
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Products;
    using eCommerceAPI.Services.Data.ProductsServices;
    using eCommerceAPI.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IProductService productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetByIdAsync(int id)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetByIdAsync"));

            return await this.productService.GetByIdAsync(id);
        }

        [HttpGet("all")]
        public IEnumerable<ProductViewModel> GetAll()
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetAll"));

            return this.productService.GetAll();
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync([FromBody] ProductFormModel productForm)
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
                this.productService.CreateAsync(productForm);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(this.Ok("Product successfully created!"));
        }

        private static string LogRequestInformation(string method, string actionName)
        {
            StringBuilder sb = new StringBuilder();

            return $"Method: {method}; Controller: ProductsController; Action: {actionName};";
        }
    }
}
