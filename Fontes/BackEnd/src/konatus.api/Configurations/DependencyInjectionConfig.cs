using Microsoft.Extensions.DependencyInjection;
using sga.utils.Interfaces;
using konatus.business.Interfaces.Repository;
using konatus.business.Interfaces.Services;
using konatus.business.Notifications;
using konatus.business.Services;
using konatus.data.Repository;
using sga.utils;
using konatus.data.Context;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using konatus.api.Interfaces;
using konatus.api.Services;

namespace scaleleads.api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repositories
            services.AddScoped<KonatusDbContext>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IStageRepository, StageRepository>();

            //Services
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IStageService, StageService>();

            //API
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}