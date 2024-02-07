
using WebAPIModels.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPIModels.Mapper.V1;
using WebAPIRepository.Interfaces.V1;
using WebAPIRepository.Managers.V1;


var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

#region Services
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
#endregion

#region AuthenticationService
//This Method use IdentityServer JWT Cofiguration
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5000;https://localhost:7001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
#endregion      

#region APISwagger 
//Swagger Versioning Configuration
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new HeaderApiVersionReader("api-verison");
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee WebAPI",
        Description = "An ASP.NET Core Web API for managing Employee items"
    });
    //options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Employee WebAPI", Version = "v2" });
});
#endregion

#region CorsSetting 
//Cors Service Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("https://Localhost:5004","https://localhost:7001")
                                           .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                                           .AllowAnyHeader()
                                           .AllowCredentials();
                    });

});
#endregion

#region  Databese&Scope
//DB Connection
builder.Services.AddDbContext<WebAPIDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("WebAPIDB"), b =>
    b.MigrationsAssembly("WebAPIModels")));

builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
#endregion

//AutoMapper Configuration
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<EmployeeProfile>();
    cfg.AddProfile<CompanyProfile>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee WebAPI v1");
        //s.SwaggerEndpoint("/swagger/v2/swagger.json", "Employee WebAPI v2");
    });
}

//This is migrate to database 
using(var scope= app.Services.CreateScope())
{
    var db=scope.ServiceProvider.GetRequiredService<WebAPIDbContext>();
    db.Database.Migrate();
}

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
app.MapControllers();

app.Run();

