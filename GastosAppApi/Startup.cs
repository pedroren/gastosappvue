using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GastosAppCoreEF.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using AutoMapper;

namespace GastosAppApi
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
            //services.AddDbContext<GastosappContext>();
            services.AddDbContext<GastosappContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("GastosappContext")));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            /*services.AddMvc().AddJsonOptions(opt =>
            {
                if (opt.SerializerSettings.ContractResolver != null)
                {
                    var resolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
                    resolver.NamingStrategy = null;
                }
            });*/
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver =
                        new DefaultContractResolver());
            /*services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });*/

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add application services.
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseRouting();
            // Enable Cors
            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute("default", "api/{controller=Values}/{action=Get}/{id?}");
                //endpoints.MapControllerRoute("code", "api/{controller}/{action=Get}/{code:string?}");
            });
            //app.UseMvc();
            /*app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=Values}/{action=Get}/{id?}");
            });*/
        }
    }
}
