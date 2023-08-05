namespace Vehicle2Go.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using System.Reflection;
    using Microsoft.AspNetCore.Identity;

    using Data.Models.User;

    using static Common.GeneralApplicationConstants;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// This method registers all services with their interfaces and implementations of given assembly.
        /// The assembly is taken from the type of random service implementation provided.
        /// </summary>
        /// <param name="serviceType">Type of random service implementation</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

                if (interfaceType == null)
                {
                    throw new InvalidOperationException(
                        $"No interface id provided for the service with name: {implementationType.Name}");
                }

                services.AddScoped(interfaceType, implementationType);
            }
        }

        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole<Guid>(AdminRoleName);

                await roleManager.CreateAsync(role);

                var adminUser = await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
