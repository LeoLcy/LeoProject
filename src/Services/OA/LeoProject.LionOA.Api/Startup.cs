using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LeoProject.Infrastructure.Filters;
using LeoProject.LionOA.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace LeoProject.LionOA.Api
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
            var assemblyName = typeof(OADbContext).Assembly.FullName+ ".Migrations";
            services.AddDbContext<OADbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"),optionsBuilder=> {
                    optionsBuilder.MigrationsAssembly(assemblyName);
                });
            });
            #region 使用redis存储
            //csredis
            RedisHelper.Initialization(new CSRedis.CSRedisClient(Configuration["Redis"]));
            #endregion
            #region 使用session保存状态
            //services.AddDistributedMemoryCache();
            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    // Make the session cookie essential
            //    options.Cookie.IsEssential = true;
            //});
            #endregion
            #region swagger ui
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerAddEnumDescriptionsDocumentFilter>();
                //c.SwaggerDoc("v1", new Info { Title = "Etor.PSIP.API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                var security = new Dictionary<string, IEnumerable<string>> { { "TokenAPI", new string[] { } }, };
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("TokenAPI", new ApiKeyScheme
                {
                    Description = "在下方输入token即可",
                    Name = "token",//默认的参数名称
                    In = "header",//默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });
            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            //app.UseSession();
            app.UseMvc();
            #region swagger ui
            if (env.IsDevelopment() || env.IsStaging())
            {
                //启用中间件服务生成Swagger作为JSON终结点
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                //启用中间件服务对swagger-ui，指定Swagger JSON终结点
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/{documentName}/swagger.json", Assembly.GetEntryAssembly().GetName().Name);
                });
            }
            #endregion
        }
    }
}
