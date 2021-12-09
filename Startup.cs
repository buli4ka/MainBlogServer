using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Server.DatabaseConfig;
using Server.Exceptions;
using Server.Services.ImageServices;
using Server.Services.PostServices;
using Server.Services.UserServices;
using Server.Utils;
using Server.Utils.Jwt;

namespace Server
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Directory Creation
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Configuration["AppSettings:PostImagesPath"]));
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Configuration["AppSettings:UserIconPath"]));
            
            // DB Connection
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // HTTPS redirection
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            
            //Serialize Jsons
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            // File Configuration
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            
            // AutoMapper Configuration
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // CORS Configuration
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials()
                            .AllowAnyMethod();
                    });
            });
            
            
            //Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainBlogServer", Version = "v1" });
            });
            
            // Setting AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            
            // Configuring relationship
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IImageService, ImageService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Swagger Configuration
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MainBlogServer v1"));
            }
            
            // File Configuration
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider =
                    new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), Configuration["AppSettings:PostImagesPath"])),
                RequestPath = new PathString("/" + Configuration["AppSettings:PostImagesPath"])
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider =
                    new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), Configuration["AppSettings:UserIconPath"])),
                RequestPath = new PathString("/" + Configuration["AppSettings:UserIconPath"])
            });
            
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();
            
            // Error and JWT Middlewares
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
