namespace eCommerce_RESTful_API.Controllers
{
    using eCommerce.InputModels.ApplicationUsers;
    using eCommerce.Services.Data.ApplicationUsersServices;
    using eCommerce.ViewModels.ApplicationUsers;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly ILogger<ApplicationUsersController> logger;
        private readonly IApplicationUserService applicationUserService;

        public ApplicationUsersController(ILogger<ApplicationUsersController> logger, IApplicationUserService applicationUserService)
        {
            this.logger = logger;
            this.applicationUserService = applicationUserService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetById(string id)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetById"));

            return await this.applicationUserService.GetById(id);
        }

        [HttpGet("all")]
        public IEnumerable<ApplicationUserViewModel> GetAll()
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetAll"));

            return this.applicationUserService.GetAll();
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync([FromBody] ApplicationUserFromModel userForm)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "CreateAsync"));

            if (!this.ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = this.ModelState
                    .Values
                    .SelectMany(model => model.Errors)
                    .Select(e => e.ErrorMessage);

                return new JsonResult(errorMessages);
            }

            try
            {
                await this.applicationUserService.CreateAsync(userForm);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(this.Ok("User successfully created."));
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] ApplicationUserCred userCred)
        {
            var token = this.applicationUserService.Authorization(userCred);

            if (token == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(token);
        }

        private static string LogRequestInformation(string method, string actionName)
        {
            return $"Method: {method}; Controller: ApplicationUsersController; Action: {actionName};";
        }
    }
}
