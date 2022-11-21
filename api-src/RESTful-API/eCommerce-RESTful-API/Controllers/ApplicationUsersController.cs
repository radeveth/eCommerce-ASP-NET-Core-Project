namespace eCommerce_RESTful_API.Controllers
{
    using AutoMapper;
    using BCrypt.Net;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.ApplicationUsers;
    using eCommerceAPI.ViewModels.ApplicationUsers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EcommerceApiDbContext dbContext;
        private readonly ILogger<ApplicationUsersController> logger;

        public ApplicationUsersController(ILogger<ApplicationUsersController> logger, EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationUserViewModel>> GetById(string id)
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetById"));

            ApplicationUser applicationUser = await this.dbContext
                .ApplicationUsers
                .FirstOrDefaultAsync(a => a.Id == id);

            return this.mapper.Map<ApplicationUserViewModel>(applicationUser);
        }

        [HttpGet("all")]
        public IEnumerable<ApplicationUserViewModel> GetAll()
        {
            this.logger.LogInformation(LogRequestInformation(this.HttpContext.Request.Method, "GetAll"));

            return this.mapper.Map<IEnumerable<ApplicationUserViewModel>>(this.dbContext.ApplicationUsers);
        }

        [HttpPost]
        public async Task<JsonResult> CreateAsync(ApplicationUserFromModel userForm)
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

            string randomSalt = BCrypt.GenerateSalt(10);
            string hashedPassword = BCrypt.HashPassword(userForm.PasswordHash, randomSalt);
            userForm.PasswordHash = hashedPassword;

            try
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Username = userForm.Username,
                    PasswordHash = userForm.PasswordHash,
                    Email = userForm.Email,
                    FullName = userForm.FullName,
                    Gender = userForm.Gender,
                };

                await this.dbContext.ApplicationUsers.AddAsync(applicationUser);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(this.Ok("User successfully created."));
        }

        private static string LogRequestInformation(string method, string actionName)
        {
            return $"Method: {method}; Controller: ApplicationUsersController; Action: {actionName};";
        }
    }
}
