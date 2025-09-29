using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBook.Infrastructure.Data;
using WebBook.Infrastructure.Seeds;
using WebBook.Infrastructure.ViewModel;
using WebBook.Permission;

namespace WebBook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            #region Seed Default User And Role

            using (var scope = app.Services.CreateScope())
            {
                var servies = scope.ServiceProvider;
                try
                {
                    var userManager = servies.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = servies.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRole.SeedAsync(roleManager);
                    await DefaultUser.SeedSuperAdminAsync(userManager, roleManager);
                    await DefaultUser.SeedBasicUserAsync(userManager, roleManager);
                    await DefaultUser.SeedClaimsAsync(roleManager);
                }
                catch (Exception)
                {

                }
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
