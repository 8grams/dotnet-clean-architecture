using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;
using SFIDWebAPI.Application.Infrastructures;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Infrastructure.Notifications.Email;
using SFIDWebAPI.Infrastructure.Notifications.SMS;
using SFIDWebAPI.Infrastructure.Notifications.FCM;
using SFIDWebAPI.Infrastructure.Persistences;
using SFIDWebAPI.Infrastructure.Authorization;
using SFIDWebAPI.Infrastructure.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace SFIDWebAPI
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
            services.AddMediatR(typeof(ForgotPasswordHandler).GetTypeInfo().Assembly);
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
            services.AddDbContext<ISFDDBContext, SFDDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("SFDDatabase")));

            // mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile(
                    services.BuildServiceProvider().GetService<ISFDDBContext>(),
                    services.BuildServiceProvider().GetService<Utils>()
                ));
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services
               .AddMvc()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ForgotPasswordCommandValidator>())
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

            // app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            app.UseAuthme();
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
