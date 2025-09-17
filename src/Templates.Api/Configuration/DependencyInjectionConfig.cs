using Templates.Api.Services;

namespace Templates.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITemplateService, TemplateService>();
        }
    }
}
