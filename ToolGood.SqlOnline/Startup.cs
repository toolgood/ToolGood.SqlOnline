using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToolGood.SqlOnline
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return Bootstrapper.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }
            Bootstrapper.Configure(app, env);

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                  name: "StaticFiles",
                  template: "_/{*url}",
                  defaults: new { controller = "StaticFiles", action = "Get" }
                 );
            });
            app.Run((c) => {
                return c.Response.WriteAsync(@"<!DOCTYPE html><html><head><meta name=""viewport"" content=""width=device-width"" /><meta charset=""utf-8""><title>404</title>
<style type=""text/css"">
.page-404 { color: #afb5bf; padding-top: 60px; padding-bottom: 90px; text-align: center; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto; }
.error-title { font-size: 80px; }
.error-description { font-size: 24px; }
p { margin-bottom: 10px; }
</style></head>
<body><div class=""page-404""><p class=""error-title""><span class=""va-m"">404</span></p><p class=""error-description"">不好意思，您访问的页面不存在~</p></div></body></html>");
            });
        }
    }
}
