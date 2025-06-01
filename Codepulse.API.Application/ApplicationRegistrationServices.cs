using Codepulse.API.Application.Features.Auth.Interfaces;
using Codepulse.API.Application.Features.Auth.Services;
using Codepulse.API.Application.Features.Blog.Interfaces;
using Codepulse.API.Application.Features.Blog.Services;
using Codepulse.API.Application.Mappers.Blog.Interfaces;
using Codepulse.API.Application.Mappers.Blog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codepulse.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogPostService, BlogPostService>(); 
            services.AddScoped<IBlogPostMapper, BlogPostMapper>();
            return services;
        }
    }
}
