using Microsoft.AspNetCore.Identity;

namespace PrintingHouse.Security
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach(var role in new[] {Roles.Operator, Roles.SeniorOperator, Roles.Manager })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
