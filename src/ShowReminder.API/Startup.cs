using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using ShowReminder.API.Manager;
using ShowReminder.Data;
using ShowReminder.TMDBFetcher.Manager;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("authentication.json")
                .AddJsonFile("tmdbKey.json")
                .AddJsonFile("dbConfig.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<AuthenticationParam>(Configuration.GetSection("authenticationParam"));


            var tmdbSettings = new TMDBSettings();
            Configuration.GetSection("tmdbSettings").Bind(tmdbSettings);
            services.AddSingleton(tmdbSettings);

            services.AddDbContext<DataContext>(
                options => options.UseMySQL(Configuration.GetSection("connectionString").Value));

            services.AddSingleton<TVManager>();
            services.AddTransient<ShowManager>();

            //Add CORS
            services.AddCors();

            // Add framework services.
            services.AddMvc();


           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseMvc();
        }
    }
}
