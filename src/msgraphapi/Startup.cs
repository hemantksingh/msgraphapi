using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using msgraphapi.ExceptionHandling;
using msgraphapi.MsGraph;

namespace msgraphapi
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfidentialClientApplication>(p =>
            {
                var logger = p.GetService<ILogger<Startup>>();
                var clientId = Configuration["AzureAD:ClientId"];
                if (clientId == null)
                {
                    logger.LogInformation("Looking up environment variable 'AZUREAD_CLIENT_ID'");
                    clientId = Environment.GetEnvironmentVariable("AZUREAD_CLIENT_ID") ??
                               throw new ArgumentException("AZUREAD_CLIENT_ID is required");
                }
                else
                {
                    logger.LogInformation("'AZUREAD_CLIENT_ID' loaded from app settings..");
                }

                var config = new AzureADClientConfig(
                    clientId,
                    Configuration["AzureAD:ClientSecret"] ??
                    Environment.GetEnvironmentVariable("AZUREAD_CLIENT_SECRET") ??
                    throw new ArgumentException("AZUREAD_CLIENT_SECRET is required"),
                    Configuration["AzureAD:TenantId"] ?? Environment.GetEnvironmentVariable("AZUREAD_TENANT_ID") ??
                    throw new ArgumentException("AZUREAD_TENANT_ID is required")
                );

                return ConfidentialClientApplicationBuilder
                    .Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(config.Authority)
                    .Build();
            });

            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(2880),
                Priority = CacheItemPriority.Normal
            };
            services.AddSingleton(cacheExpirationOptions);
            services.AddSingleton<IPagedCache<Group>, PagedCache<Group>>();
            services.AddSingleton<IPagedCache<Domain>, PagedCache<Domain>>();
            services.AddSingleton<AzureAdCache>();
            services.AddTransient<MsGraphClient>();
            services.AddTransient<TokenService>();
            services.AddTransient<DomainsService>();
            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .AddNewtonsoftJson();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/azuread");
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("azuread/swagger/v1/swagger.json", "MS Graph API"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}