using BlenderParadise.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlenderParadise.Infrastucture
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder SeedAdmin(
            this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    IdentityRole role = new IdentityRole("Administrator");
                    await roleManager.CreateAsync(role);
                }

                ApplicationUser admin = await userManager.FindByEmailAsync("administrator@gmail.com");
                await userManager.AddToRoleAsync(admin, "Administrator");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
