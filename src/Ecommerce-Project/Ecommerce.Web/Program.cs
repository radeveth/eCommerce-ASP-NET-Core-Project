namespace Ecommerce.Web
{
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Data.Seeder;
    using Ecommerce.Services.Data.AdminServices;
    using Ecommerce.Services.Data.ApplicationUsersServices;
    using Ecommerce.Services.Data.BrandsServices;
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.HomeServices;
    using Ecommerce.Services.Data.OrdersServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Ecommerce.Services.Data.ReviewsServices;
    using Ecommerce.Services.Data.ShoppingCardsServices;
    using Ecommerce.Services.Mappings;
    using Ecommerce.Web.Settings;
    using Microsoft.EntityFrameworkCore;
    using Stripe;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<ApplicationProfile>();
            });

            builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddTransient<IBrandService, BrandService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IHomeService, HomeService>();
            builder.Services.AddTransient<IProductService, Ecommerce.Services.Data.ProductsServices.ProductService>();
            builder.Services.AddTransient<IProductWishlistService, ProductWishlistService>();
            builder.Services.AddTransient<IReviewService, Ecommerce.Services.Data.ReviewsServices.ReviewService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IShoppingCardService, ShoppingCardService>();

            //builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<EcommerceDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "areaRoute",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}