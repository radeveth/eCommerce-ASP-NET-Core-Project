namespace Ecommerce.Services.Data.ApplicationUsersServices
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using AutoMapper;
    using BCrypt.Net;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.ApplicationUsers;
    using Ecommerce.ViewModels.ApplicationUsers;
    using IdGen;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper mapper;
        private readonly EcommerceDbContext dbContext;
        private readonly string tokenKey = "My test token key";

        public ApplicationUserService(EcommerceDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApplicationUserViewModel> GetById(string id)
        {
            ApplicationUser applicationUser = await this.dbContext
                .ApplicationUsers
                .FirstOrDefaultAsync(a => a.Id == id);

            return this.mapper.Map<ApplicationUserViewModel>(applicationUser);
        }

        public IEnumerable<ApplicationUserViewModel> GetAll()
        {
            return this.mapper.Map<IEnumerable<ApplicationUserViewModel>>(this.dbContext.ApplicationUsers);
        }

        public async Task CreateAsync(ApplicationUserFromModel userForm)
        {
            string randomSalt = BCrypt.GenerateSalt(10);
            string hashedPassword = BCrypt.HashPassword(userForm.PasswordHash, randomSalt);
            userForm.PasswordHash = hashedPassword;

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = GenerateId(),
                Username = userForm.Username,
                PasswordHash = userForm.PasswordHash,
                Email = userForm.Email,
                FullName = userForm.FullName,
                Gender = userForm.Gender,
            };

            await this.dbContext.ApplicationUsers.AddAsync(applicationUser);
            await this.dbContext.SaveChangesAsync();
        }

        public string Authorization(ApplicationUserCred userCred)
        {
            foreach (var user in this.dbContext.ApplicationUsers)
            {
                if ((bool)BCrypt.Verify(userCred.Password, user.PasswordHash) && user.Username == userCred.Username)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(this.tokenKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, userCred.Username),
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
            }

            return null;
        }

        private static bool? ValidatePassword(string password, string correctHash)
        {
            bool result = BCrypt.Verify(password, correctHash);

            return result == false ? null : true;
        }

        private static string GenerateId()
        {
            var generator = new IdGenerator(0);
            var id = generator.CreateId();

            return id.ToString();
        }
    }
}
