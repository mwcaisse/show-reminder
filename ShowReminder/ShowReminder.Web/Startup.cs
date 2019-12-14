using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShowReminder.Data;
using ShowReminder.TMDBFetcher.Manager;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;
using ShowReminder.Web.Manager;
using ShowReminder.Web.Mapper;
using ShowReminder.Web.Models;
using ShowReminder.Web.Scheduler;
using ShowReminder.Web.Scheduler.Jobs;

namespace ShowReminder.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("deploymentProperties.json", optional: false, reloadOnChange: true)
                .AddJsonFile("authentication.json")
                .AddJsonFile("tmdbKey.json")
                .AddJsonFile("dbConfig.json")
                .AddJsonFile("settings.json")
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

            var apiUrl = Configuration.GetValue("apiUrl", "");
            var rootPathPrefix = Configuration.GetValue("rootPathPrefix", "");

            services.AddSingleton<DeploymentProperties>(new DeploymentProperties()
            {
                ApiUrl = apiUrl,
                RootPathPrefix = rootPathPrefix
            });
            
            var applicationConfiguration = new ApplicationConfiguration()
            {
                SendGridApiKey = Configuration.GetValue<string>("sendGridApiKey"),
                FromEmailAddress = Configuration.GetValue<string>("fromEmailAddress"),
                FromEmailAddressName = Configuration.GetValue<string>("fromEmailAddressName"),
                ToEmailAddress = Configuration.GetValue<string>("toEmailAddress"),
                ToEmailAddressName = Configuration.GetValue<string>("toEmailAddressName")
            };

            services.AddSingleton(applicationConfiguration);

            services.AddDbContext<DataContext>(
                options => options.UseMySql(Configuration.GetSection("connectionString").Value));

            services.AddSingleton<TVManager>();
            services.AddTransient<ShowManager>();
            services.AddTransient<TrackedShowManager>();
            services.AddSingleton<EmailManager>();


            // Add framework services.
            services.AddMvc();

            //Add Jobs
            services.AddTransient<UpdateExpiredShowsJob>();

            services.AddSingleton<QuartzScheduler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IApplicationLifetime lifetime,
            IServiceProvider container)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var scheduler = container.GetService<QuartzScheduler>();
            lifetime.ApplicationStarted.Register(scheduler.Start);
            lifetime.ApplicationStopping.Register(scheduler.Stop);
        }
    }
}
