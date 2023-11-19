using System.Collections.Generic;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace E_PropertyCMS.Repository;
public class CoreContext : DbContext, ICoreContext
{

    public CoreContext()
    {
    }

    public DbSet<ClientDbModel> Client { get; set; }
    public DbSet<PropertyDbModel> Property { get; set; }
    public DbSet<AddressDbModel> Address { get; set; }
    public DbSet<RoomDbModel> Room { get; set; }
    public DbSet<CaseDbModel> Case { get; set; }
    public DbSet<CaseTypeDbModel> CaseType { get; set; }
    public DbSet<CaseStatusDbModel> CaseStatus { get; set; }

    public CoreContext(DbContextOptions<CoreContext> options)
        : base(options)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        public CoreContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "E-PropertyCMS.API"))
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<CoreContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString);

            return new CoreContext(builder.Options);
        }
    }

}

