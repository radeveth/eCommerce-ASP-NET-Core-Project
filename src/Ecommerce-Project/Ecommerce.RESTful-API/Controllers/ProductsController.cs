namespace Ecommerce.RESTful_API.Controllers
{
    using System.Text;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

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

            return this.productService.GetByIdAsync<ProductViewModel>(id);
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
                await this.productService.CreateAsync(productForm);
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
