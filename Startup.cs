using System;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using WebApi.Application.Interfaces;
using WebApi.Application.Infrastructures.AutoMapper;
using WebApi.Application.Infrastructures;
using WebApi.Application.UseCases.User.Command.CreateUser;
using WebApi.Application.Interfaces.Authorization;
using WebApi.Application.Misc;
using WebApi.Infrastructure.Notifications.Email;
using WebApi.Infrastructure.Notifications.SMS;
using WebApi.Infrastructure.Notifications.FCM;
using WebApi.Infrastructure.Persistences;
using WebApi.Infrastructure.Authorization;
using WebApi.Infrastructure.FileManager;
using WebApi.Infrastructure.ErrorHandler;

namespace WebApi
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

            // Add MediatR
            services.AddMediatR(typeof(CreateUserCommandHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            services.AddTransient<IFCMService, FCMService>();
            services.AddSingleton<Utils>();
            services.AddHttpContextAccessor();

            // setting
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<SMSSetting>(Configuration.GetSection("SMSSettings"));

            services.AddHttpClient();

            // Add DbContext using SQL Server Provider
            services.AddDbContext<IWebApiDbContext, WebApiDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseMySql(Configuration.GetConnectionString("WebApiDatabase"), new MySqlServerVersion(new Version(8, 0, 26))));

            // mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile(
                    services.BuildServiceProvider().GetService<IWebApiDbContext>(),
                    services.BuildServiceProvider().GetService<Utils>()
                ));
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services
               .AddMvc()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>())
               .AddNewtonsoftJson(x =>
               {
                   x.SerializerSettings.ContractResolver = new DefaultContractResolver
                   {
                       NamingStrategy = new SnakeCaseNamingStrategy()
                       
                   };
                   x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               })
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = c =>
                   {
                       var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                           .SelectMany(v => v.Errors)
                           .Select(v => v.ErrorMessage));

                       return new BadRequestObjectResult(new
                       {
                           ErrorCode = 400,
                           Message = errors
                       });
                   };
               });

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ISMSService, SMSService>();
            services.AddScoped<IAuthUser, AuthUser>();
            services.AddScoped<IAuthAdmin, AuthAdmin>();
            services.AddSingleton<IMemoryData, MemoryData>();
            services.AddScoped<IUploader, ManagedDiskUploader>();

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                    RequestPath = "/uploads"
            });

            app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            app.UseAuthme();
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
