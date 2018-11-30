using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ToolGood.Infrastructure;
using ToolGood.Infrastructure.Dependency;
using ToolGood.Infrastructure.Dependency.AutofacContainer;

namespace ToolGood.SqlOnline
{
    public class Bootstrapper
    {
        public static IContainer AutofacContainer;

        public static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddSession(o => {
                o.IdleTimeout = TimeSpan.FromDays(1);
                o.Cookie.Name = "sid";
                o.Cookie.IsEssential = true;
                o.Cookie.HttpOnly = true;
                o.Cookie.SameSite = SameSiteMode.None;
            });

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                 .AddJsonOptions(options => {
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                     options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; //设置缩进
                     //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;//忽略空值
                     options.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

                     options.SerializerSettings.Converters.Add(new JsonCustomDoubleConvert());// json序列化时， 防止double，末尾出现小数点浮动,
                     options.SerializerSettings.Converters.Add(new JsonCustomDoubleNullConvert());// json序列化时， 防止double，末尾出现小数点浮动,
                 });
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var thisAssembly = typeof(Bootstrapper).Assembly;
            var dir = Path.GetDirectoryName(thisAssembly.Location);

            var repositoryAssembly = File.Exists(Path.Combine(dir, "ToolGood.Repository.dll")) ? Assembly.Load("ToolGood.Repository") : thisAssembly;
            var applicationAssembly = File.Exists(Path.Combine(dir, "ToolGood.Application.dll")) ? Assembly.Load("ToolGood.Application") : thisAssembly;
            var domainServiceAssembly = File.Exists(Path.Combine(dir, "ToolGood.DomainService.dll")) ? Assembly.Load("ToolGood.DomainService") : thisAssembly;


            ContainerManager.UseAutofacContainer(builder)
                            .RegisterAssemblyTypes(repositoryAssembly, m => m.Namespace != null && m.Namespace.StartsWith("ToolGood.Repository.Impl") && m.Name.EndsWith("Repository"), lifeStyle: LifeStyle.PerLifetimeScope)
                            .RegisterAssemblyTypes(domainServiceAssembly, m => m.Namespace != null && m.Namespace.StartsWith("ToolGood.DomainService.Impl") && m.Name.EndsWith("Service"), lifeStyle: LifeStyle.PerLifetimeScope)
                            .RegisterAssemblyTypes(applicationAssembly, m => m.Namespace != null && m.Namespace.StartsWith("ToolGood.Application.Impl") && m.Name.EndsWith("Application"), lifeStyle: LifeStyle.PerLifetimeScope)
                            ;

            var t = ContainerManager.Instance.Container.BeginLeftScope();

            var autofacObjectContainer = ContainerManager.Instance.Container as AutofacObjectContainer;
            AutofacContainer = autofacObjectContainer.Container;



            //使用容器创建 AutofacServiceProvider 
            return new AutofacServiceProvider(AutofacContainer);

        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            HostingEnvironment.ApplicationName = env.ApplicationName;
            HostingEnvironment.ContentRootPath = env.ContentRootPath;
            HostingEnvironment.EnvironmentName = env.EnvironmentName;
            HostingEnvironment.WebRootPath = env.WebRootPath;
        }


        public static void UnInstall()
        {
            //Configuration.UnInstall();
        }


        /// <summary>
        /// json序列化时， 防止double，末尾出现小数点浮动
        /// </summary>
        public class JsonCustomDoubleConvert : CustomCreationConverter<double>
        {
            public override bool CanWrite { get { return true; } }
            public override double Create(Type objectType) { return 0.0; }
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { return reader.Value; }
            /// <summary>
            /// 重载序列化方法
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="value"></param>
            /// <param name="serializer"></param>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null) {
                    writer.WriteNull();
                } else {
                    decimal d = Math.Round((decimal)((double)value), 10);
                    writer.WriteValue(d);
                }
            }
        }
        public class JsonCustomDoubleNullConvert : CustomCreationConverter<double?>
        {
            public override bool CanWrite { get { return true; } }
            public override double? Create(Type objectType) { return null; }
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { return reader.Value; }
            /// <summary>
            /// 重载序列化方法
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="value"></param>
            /// <param name="serializer"></param>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null || ((double?)value).HasValue == false) {
                    writer.WriteNull();
                } else {
                    decimal d = Math.Round((decimal)((double?)value).Value, 10);
                    writer.WriteValue(d);
                }
            }
        }

    }
}
