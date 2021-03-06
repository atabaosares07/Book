using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Api.Helpers;
using Book.AutoMapper;
using Book.Data;
using Book.LoggerProvider;
using Book.Services;
using Book.Services.Base;
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
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Book.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AutoMapperConfiguration.Configure();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);

            services.AddDbContextPool<DataContext>(options => options
                .UseSqlServer(appSettings.ConnectionString));

            services.AddControllers();

            services.AddCors();

            services.AddJwtAuthentication(appSettings.Secret);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>(false);
            });

            services.AddTransient<DataContext>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<LoggerProvider.ILogger, Logger>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwaggerUI(o =>
            {
                o.RoutePrefix = "api";
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
                o.OAuthClientId("swagger-ui");
                o.OAuthClientSecret("swagger-ui-secret");
                o.OAuthRealm("swagger-ui-realm");
                o.OAuthAppName("Swagger UI");
                o.InjectStylesheet("/swagger-ui/custom.css");
                o.InjectJavascript("/swagger-ui/custom.js");
            });

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
