namespace eCommerce_RESTful_API.Controllers
{
    using eCommerceAPI.InputModels.Brands;
    using eCommerceAPI.Services.Data.BrandsServices;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Text;

    [Route("api/[controller]/")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService, ILogger<ProductsController> logger)
        {
            this.brandService = brandService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync([FromBody] BrandFormModel brandForm)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "CreateAsync"));

            if (!ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = this.ModelState
                    .Values
                    .SelectMany(modelState => modelState.Errors)
                    .Select(error => error.ErrorMessage);

                return new JsonResult(errorMessages);
            }

            try
            {
                await this.brandService.CreateAsync(brandForm);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(this.Ok("Brand successfully created!"));
        }

        private static string LogRequestInformation(string method, string actionName)
        {
            StringBuilder sb = new StringBuilder();

            return $"Method: {method}; Controller: BrandsController; Action: {actionName};";
        }
    }
}
