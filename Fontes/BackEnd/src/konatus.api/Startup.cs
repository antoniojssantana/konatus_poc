using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using scaleleads.api.Configurations;
using konatus.api.Extensions;
using konatus.api.HostedServices;
using konatus.business.Options;
using konatus.data.Context;
using sga.utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konatus.api.Configurations;

namespace konatus.api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
               .AddEnvironmentVariables()
               .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailConfiguration"));

            services.Configure<FolderSettings>(Configuration.GetSection("FolderSettings"));

            services.AddDbContext<KonatusDbContext>(options =>
                   options.UseNpgsql(Environment.ExpandEnvironmentVariables(Configuration.GetConnectionString("Postgresql"))
                   .Replace("%POSTGRES_HOST%", "localhost"),
                           p => p.EnableRetryOnFailure(
                           maxRetryCount: 6,
                           maxRetryDelay: TimeSpan.FromSeconds(5),
                           errorCodesToAdd: null)));
            services.Configure<DatabaseOptions>(Configuration.GetSection(DatabaseOptions.SECTION))
                .PostConfigure<DatabaseOptions>(x => x.Postgresql = Environment.ExpandEnvironmentVariables(x.Postgresql).Replace("%POSTGRES_HOST%", "localhost"));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "konatus.api", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.ResolveDependecies();

            services.AddIdentityConfiguration(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("Livre", builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());

                options.AddPolicy("Prod", builder => builder
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                                        .WithOrigins("https://signapp.3adc.com.br", "http://localhost")
                                        .AllowCredentials());
            });

            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            if (Configuration.GetValue<bool>("RunMigration"))
            {
                services.AddHostedService<MigrationHostedService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("Livre");
            }
            else
            {
                app.UseCors("Prod");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "konatus.api v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHsts();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}