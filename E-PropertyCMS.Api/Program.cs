using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Repository;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<ICoreContext, CoreContext>();
builder.Services.AddTransient<IUnitOfWork, CoreContext>();
builder.Services.AddTransient<IClientContext, CoreContext>();
builder.Services.AddTransient<IPropertyContext, CoreContext>();
builder.Services.AddTransient<ICaseContext, CoreContext>();

builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ICaseRepository, CaseRepository>();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ICaseService, CaseService>();

builder.Services.AddDbContext<CoreContext>(opt => opt.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-PropertyCMS.Api"));
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();