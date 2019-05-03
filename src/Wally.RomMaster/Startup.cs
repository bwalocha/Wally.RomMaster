using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RomMaster.BusinessLogic.Services;
using Wally.RomMaster.Database;
using Wally.RomMaster.DatFileParser;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddOptions()
                .Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlite(Configuration.GetSection("AppSettings")
                            .GetConnectionString("sqlite"))
                        .EnableSensitiveDataLogging(false);
                }, ServiceLifetime.Singleton)

                .AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>()

                .AddSingleton<Parser>()
                .AddSingleton<FileWatcherService>()
                .AddSingleton<DatFileService>()
                .AddSingleton<RomFileService>()
                .AddSingleton<ToSortFileService>()
                .AddSingleton<FixService>()
                .AddSingleton<HashAlgorithm, Force.Crc32.Crc32Algorithm>()
                .AddSingleton<IDebuggerService, DebuggerService>()
                .AddSingleton<IHostedService, ClientService>()
                .Replace(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(TimedLogger<>)))
                ;

            // ILoggerFactory AddProvider
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(app.ApplicationServices.GetService<IDebuggerService>().LoggerProvider);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
