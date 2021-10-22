using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammerGrammar.WispyWaterfall.Core;
using ProgrammerGrammar.WispyWaterfall.Infrastructure;

namespace ProgrammerGrammar.WispyWaterfall.Web
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
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.Load("WispyWaterfall.Core"), Assembly.Load("WispyWaterfall.Infrastructure"))
                .AddMediatR(Assembly.GetExecutingAssembly());

            services
                .AddCore()
                .AddInfrastructure(Configuration);

            services
                .AddAuthentication()
                .AddJwtBearer();

            services
                .AddControllers();

            services
                .AddSpaStaticFiles(configuration => configuration.RootPath = "dist");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // for development only
            // chrome requires secure cookies w/ http, so let's make it lax
            if (env.IsDevelopment())
                app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            else
                app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
                app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "./";

                if (env.IsDevelopment())
                    spa.UseProxyToSpaDevelopmentServer("http://wispywaterfall.webapp:4200");
            });
        }
    }
}