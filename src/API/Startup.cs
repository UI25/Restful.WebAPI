using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using WebAPIModels.Mapper.V1;
using WebAPIModels.Data;
using WebAPIRepository.Interfaces.V1;
using WebAPIRepository.Managers.V1;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using WebAPIModels.Models.V1;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using Microsoft.VisualBasic;

namespace WebAPI
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        //This method gets called by the runtime. Use this method add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            //https port cofiguration 
            services.AddHttpsRedirection(opts => 
            {
                 opts.HttpsPort = 5001;
            });

            //This Method use IdentityServer JWT Cofiguration
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5000;https://localhost:7001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
             //End        
            //Host Configuration
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
            //End 
           //Swagger Versioning Configuration
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new HeaderApiVersionReader("api-verison");
            });
            //Cors Service Configuration
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                policy =>
                                        {
                                            policy.WithOrigins("https://Localhost:5004",
                                                               "https://localhost:7001")
                                                   .WithMethods("GET", "POST", "PUT", "DELETE","OPTIONS")
                                                   .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                   .AllowAnyHeader()
                                                   .AllowCredentials();
                                        });
                                    
            });
            //End
            //Swagger Versioning Configuration
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1",
                    Title = "Employee WebAPI",
                    Description = "An ASP.NET Core Web API for managing Employee items"
                });
                //options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Employee WebAPI", Version = "v2" });
            });
            //End
            //AutoMapper Configuration
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<CompanyProfile>();
            });
            //End
            //DB Connection
            services.AddDbContext<WebAPIDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WebAPIDB"), b=>
                b.MigrationsAssembly("CompanyEmployees")));
            
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            //services.AddSingleton<IAuthorizationHandler, MyHandler>();

        }
        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();

                //Swagger Configuration
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee WebAPI v1");
                    //s.SwaggerEndpoint("/swagger/v2/swagger.json", "Employee WebAPI v2");
                });
            }
            else
            {
                app.UseExceptionHandler();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("MyAllowSpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();
            /*
            app.UseAuthorization(options =>
                 options.AddPolicy("Read", 
                 policy =>policy.RequireClaim("scope", "api.read","")));
            */            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/echo",
                    context => context.Response.WriteAsync("echo"))
                                    .RequireCors("MyAllowSpecificOrigins");
                endpoints.MapControllers()
                                    .RequireCors("MyAllowSpecificOrigins"); 
                endpoints.MapRazorPages()
                                    .RequireCors("MyAllowSpecificOrigins");
            });
        }

    }
}
