namespace eCommerce_RESTful_API
{
    using System.Text;
    using eCommerce.Data;
    using eCommerce.Data.Seeder;
    using eCommerce.Services.Data.ApplicationUsersServices;
    using eCommerce.Services.Data.BrandsServices;
    using eCommerce.Services.Data.CategoriesServices;
    using eCommerce.Services.Data.ProductsServices;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using static eCommerce.Services.Mappings.ApplicationProfile;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddDbContext<EcommerceApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<ProductsProfile>();
                config.AddProfile<CategoriesProffile>();
                config.AddProfile<ImageProfile>();
                config.AddProfile<ApplicationUsersProffile>();
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddTransient<IBrandService, BrandService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var tokenKey = "My test token key";
            var key = Encoding.ASCII.GetBytes(tokenKey);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EcommerceApiDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}