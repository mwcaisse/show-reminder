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
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using Serilog;
using ShowReminder.Data;
using ShowReminder.TMDBFetcher.Manager;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.Web.Manager;
using ShowReminder.Web.Mapper;
using ShowReminder.Web.Models;
using ShowReminder.Web.Scheduler;
using ShowReminder.Web.Scheduler.Jobs;
using ShowReminder.Web.Utils;

namespace ShowReminder.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("application.json", true)
                .AddJsonFile($"application.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables("SHOW_REMINDER_API_");
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            var databaseConfiguration = new DatabaseConfiguration()
            {
                Host = Configuration.GetValue<string>("database:host"),
                User = Configuration.GetValue<string>("database:user"),
                Password = Configuration.GetValue<string>("database:password"),
                Port = Configuration.GetValue<string>("database:port"),
                Schema = Configuration.GetValue<string>("database:schema"),
                SslMode = Configuration.GetValue<string>("database:sslMode")
            };
            services.AddSingleton(databaseConfiguration);

            var tmdbSettings = new TMDBSettings()
            {
                ApiKey = Configuration.GetValue<string>("tmdb:apiKey"),
                BaseUrl = Configuration.GetValue<string>("tmdb:baseUrl")
            };
            services.AddSingleton(tmdbSettings);

            var applicationConfiguration = new ApplicationConfiguration()
            {
                SendGridApiKey = Configuration.GetValue<string>("email:sendGridApiKey"),
                FromEmailAddress = Configuration.GetValue<string>("email:fromAddress"),
                FromEmailAddressName = Configuration.GetValue<string>("email:fromName"),
                ToEmailAddress = Configuration.GetValue<string>("email:toAddress"),
                ToEmailAddressName = Configuration.GetValue<string>("email:toName")
            };

            services.AddSingleton(applicationConfiguration);

            Log.Information("Migrating our database in startup yo.");
            DatabaseHelper.MigrateDatabase(databaseConfiguration.ConnectionString);
            
            services.AddDbContext<DataContext>(options =>
                options.UseMySql(
                    databaseConfiguration.ConnectionString,
                    ServerVersion.AutoDetect(databaseConfiguration.ConnectionString)
                ));

            services.AddSingleton<TVManager>();
            services.AddTransient<ShowManager>();
            services.AddTransient<TrackedShowManager>();
            services.AddSingleton<EmailManager>();


            // Add framework services.
            services.AddMvc();

            //Add Jobs
            services.AddTransient<UpdateExpiredShowsJob>();
            services.AddTransient<SendEmailRecentlyAiredShowsJob>();

            services.AddSingleton<QuartzScheduler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IApplicationLifetime lifetime,
            IServiceProvider container)
        {
            loggerFactory.AddSerilog();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(routes => { routes.MapControllers(); });

            var scheduler = container.GetService<QuartzScheduler>();
            lifetime.ApplicationStarted.Register(scheduler.Start);
            lifetime.ApplicationStopping.Register(scheduler.Stop);
        }
    }
}
