using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;

namespace msgraphapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AzureADClientConfig(
                Environment.GetEnvironmentVariable("AZUREAD_CLIENT_ID") ?? throw new ArgumentException("AZUREAD_CLIENT_ID is required"),
                Environment.GetEnvironmentVariable("AZUREAD_CLIENT_SECRET") ?? throw new ArgumentException("AZUREAD_CLIENT_SECRET is required"),
                Environment.GetEnvironmentVariable("AZUREAD_TENANT_ID") ?? throw new ArgumentException("AZUREAD_TENANT_ID is required"));

            services.AddSingleton<IConfidentialClientApplication>(_ => ConfidentialClientApplicationBuilder
                .Create(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthority(config.Authority)
                .Build());
            services.AddTransient<TokenService>();
            services.AddMvc().AddNewtonsoftJson();
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

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MS Graph API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
