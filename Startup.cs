using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using ZhoskiyBenchSharp.Models;

namespace ZhoskiyBenchSharp
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ZhoskiyBenchSharp", Version = "v1"});
            });

            var connectionString = Configuration["MYSQL_CONNECTION_STRING"];
            var serverVersion = new MariaDbServerVersion(new Version(10, 5, 0));
            
            services.AddDbContext<AppContext>(
                options =>
                {
                    options.UseMySql(connectionString, serverVersion);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZhoskiyBenchSharp v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            var migrirovatDb = Configuration["ZHOSKA_MIGRIROVAT_DB"];

            if (migrirovatDb == "true")
            {
                Console.WriteLine("Жоска мигрирую базу");
                context.Database.Migrate();
                Console.WriteLine("Жоска мигрировал");
            }
            
            var napolnitDb = Configuration["ZHOSKA_NAPOLNIT_DB"];

            if (napolnitDb == "true")
            {
                Console.WriteLine("Жоска наполняю базу");
                var bears = new Bear[5000];

                for (int i = 0; i < bears.Length; i++)
                {
                    bears[i] = new Bear
                    {
                        Name = "Sanes",
                        KdRatio = "5/1",
                        LoveToSuckCocks = true
                    };
                }

                context.Bears.AddRange(bears);
                context.SaveChanges();
                Console.WriteLine("Жоска наполнил базу");
            }
        }
    }
}