using Microsoft.AspNetCore.Identity;

namespace pj_banco_quest.Models
{
    public class Roles_DB
    {
        public static class Roles
        {
            public const string Aluno = "Aluno";
            public const string Professor = "Professor";
            public const string Administrador = "Administrador";
        }

        public static class RoleInitializer
        {
            public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
            {
                using var scope = serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                // Defina os nomes das roles
                string[] roleNames = { Roles.Aluno, Roles.Professor, Roles.Administrador };

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
        }

    }
}
