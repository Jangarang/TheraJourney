using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Core.ServiceContracts;
using TheraJournal.Core.Services;
using TheraJournal.Infrastructure.DbContext;
using TheraJournal.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));

    //Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters(); //What does this do?


builder.Services.AddTransient<IJwtService, JwtService>(); //what is transient?

// Interface registration
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();

builder.Services.AddApiVersioning(config => {
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); //api version as per the request URL

    config.DefaultApiVersion = new ApiVersion(1, 0); 
    config.AssumeDefaultVersionWhenUnspecified = true;
    //config.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOpenApi();

//Db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
// Read from env
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "API V1", Version = "v1", Description = "This is version 1" });
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

//CORS
#region cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => {
            policy
            .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
        });
});
#endregion

//Identity
#region identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser,ApplicationRole,ApplicationDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();
#endregion

//JWT
#region jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).
    AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")
                           ?? throw new InvalidOperationException("JWT Key not configured")
            ))
        };
    });
builder.Services.AddAuthorization(options => { });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

//Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2"); 
});

//
app.UseRouting();
app.UseCors("AllowReact");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
