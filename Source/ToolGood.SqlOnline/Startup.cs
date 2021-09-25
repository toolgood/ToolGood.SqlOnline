/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ToolGood.Common.Internals;
using ToolGood.SqlOnline.Commons;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Extensions;

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
        public void ConfigureServices(IServiceCollection services)
        {
            MyIoc.SetServiceCollection(services);
            MyHttpContext.SetServiceCollection(services);
            services.RegisterAssemblyInterfaces("ToolGood.SqlOnline.Application", null, LifeStyle.PerLifetimeScope);

            services.AddAntiforgery(options => {
                options.HeaderName = "__RequestVerificationToken";
                options.FormFieldName = "__RequestVerificationToken";
            });

            services.AddResponseCompression(options => {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml", "application/x-font-truetype" });
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.Cookie.Name = "sid";
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.IOTimeout = TimeSpan.FromSeconds(1);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.Path = "/";
                //options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None; //猎豹浏览器 ajax 请求无效
                //options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            });
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => false;
                //options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None; //猎豹浏览器 ajax 请求无效
                //options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            });
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddControllersWithViews(options => {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
                    options.SerializerSettings.Converters.Add(new JsonCustomDoubleConvert());// json序列化时， 防止double，末尾出现小数点浮动,
                    options.SerializerSettings.Converters.Add(new JsonCustomDoubleNullConvert());// json序列化时， 防止double，末尾出现小数点浮动,
                });

            services.AddRazorPages();
            services.AddControllers();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            MyHostingEnvironment.ApplicationName = env.ApplicationName;
            MyHostingEnvironment.ContentRootPath = env.ContentRootPath;
            MyHostingEnvironment.EnvironmentName = env.EnvironmentName;
            MyHostingEnvironment.WebRootPath = env.WebRootPath;
            MyHostingEnvironment.IsDevelopment = env.IsDevelopment();
            Bootstrap.Init(app.ApplicationServices, env);

            app.UseIpFirewall();
            app.UseResponseCompression();
            app.UseResponseCaching();

            #region UselLetsEncrypt
            app.UseStaticFiles();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                var path = Path.Combine(env.ContentRootPath, ".well-known");
                if (Directory.Exists(path) == false) {
                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(Path.Combine(path, "Check"));//防止 .well-known 被删除
                }
                app.UseStaticFiles(new StaticFileOptions {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = new PathString("/.well-known"),
                    ServeUnknownFileTypes = true
                });
            }

            #endregion
            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            app.Run((c) => {
                return c.Response.WriteAsync(@"<!DOCTYPE html><html><head><meta name=""viewport"" content=""width=device-width"" /><meta charset=""utf-8""><title>404</title>
<style type=""text/css"">
.page-404 { color: #afb5bf; padding-top: 60px; padding-bottom: 90px; text-align: center; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto; }
.error-title { font-size: 80px; }
.error-description { font-size: 24px; }
p { margin-bottom: 10px; }
</style></head>
<body><div class=""page-404""><p class=""error-title""><span class=""va-m"">404</span></p><p class=""error-description"">不好意思，您访问的页面不存在~</p><p class=""error-description"">Sorry, the page you visited does not exist~</p></div></body></html>");
            });
        }
    }
}
