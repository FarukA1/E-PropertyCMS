using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Auth0.ManagementApi;
using E_PropertyCMS.Api.Auth;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Caching;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Repository;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Api-Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Api-Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy(
        "read:messages",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:messages", builder.Configuration["Api-Auth0:Domain"])
        )
      );
  });

//builder.Services.AddAuth0WebAppAuthentication(options =>
//{
//    options.Domain = builder.Configuration["Web-Auth0:Domain"];
//    options.ClientId = builder.Configuration["Web-Auth0:ClientId"];
//});

builder.Services.AddAuth0AuthenticationClient(config =>
{
    config.Domain = builder.Configuration["AccountManagement-Auth0:Domain"];
    config.ClientId = builder.Configuration["AccountManagement-Auth0:ClientId"];
    config.ClientSecret = builder.Configuration["AccountManagement-Auth0:ClientSecret"];
});

builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddScoped<ICoreContext, CoreContext>();

builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ICaseRepository, CaseRepository>();
builder.Services.AddTransient<IPropertyRepository, PropertyRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IDtoToDomain, DtoToDomain>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ICaseService, CaseService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<CoreContext>(opt => opt.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddMemoryCache();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(opt =>
{
    var accessor = opt.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-PropertyCMS.Api", Version = "v1" });
});

var corsPolicy = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        builder =>
        {
            builder.
            AllowAnyOrigin().
            AllowAnyMethod().
            AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-PropertyCMS.Api"));
}

app.UseRouting();

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();