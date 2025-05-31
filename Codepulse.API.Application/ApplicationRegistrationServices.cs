using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codepulse.API.Application.Features.Auth.Interfaces;
using Codepulse.API.Application.Features.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codepulse.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
