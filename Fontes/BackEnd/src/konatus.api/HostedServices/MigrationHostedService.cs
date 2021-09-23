using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using konatus.data.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using konatus.api.Data;

namespace konatus.api.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            using var ctxIdentity = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            using var ctxRepository = scope.ServiceProvider.GetRequiredService<KonatusDbContext>();
            await ctxIdentity.Database.MigrateAsync();
            await ctxRepository.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}