using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;
using WTBeiboot_SS21_Albus.Logger;


namespace WTBeiboot_SS21_Albus
{
    public class Startup
    {
        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var projectInfo = Configuration.GetSection("Project");

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(
                swaggerOptions =>
                {
                    swaggerOptions.SwaggerDoc("WTBeiboot_APISpecification",
                        new OpenApiInfo
                        {
                            Title = projectInfo.GetSection("Title").Value,
                            Version = "V1",
                            Description = projectInfo.GetSection("Description").Value,
                            Contact = new OpenApiContact()
                            {
                                Name = projectInfo.GetSection("Name").Value,
                                Email = projectInfo.GetSection("Email").Value
                            }
                        });


                });

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddControllers();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<MicroserviceContainerModule>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/WTBeiboot_APISpecification/swagger.json", "WTBeiboot SS21 Albus V1");
            });

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
