namespace Ecommerce.Services.Data.ApplicationUsersServices
{
    using Ecommerce.InputModels.ApplicationUsers;
    using Ecommerce.ViewModels.ApplicationUsers;

    public interface IApplicationUserService
    {
        public Task<ApplicationUserViewModel> GetById(string id);

        public IEnumerable<ApplicationUserViewModel> GetAll();

        public Task CreateAsync(ApplicationUserFromModel userForm);

        public string Authorization(ApplicationUserCred userCred);
    }
}
