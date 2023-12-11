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
    public DbSet<UserDbModel> User { get; set; }

    public CoreContext(DbContextOptions<CoreContext> options)
        : base(options)
    {

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<ClientDbModel>().ToTable("Client");
        modelBuilder.Entity<ClientDbModel>().Property(t => t.Title).HasMaxLength(250);
        modelBuilder.Entity<ClientDbModel>().Property(t => t.FirstName).HasMaxLength(250);
        modelBuilder.Entity<ClientDbModel>().Property(t => t.LastName).HasMaxLength(250);
        modelBuilder.Entity<ClientDbModel>().Property(t => t.Email).HasMaxLength(50);
        modelBuilder.Entity<ClientDbModel>().Property(t => t.Phone).HasMaxLength(20);
        modelBuilder.Entity<ClientDbModel>().HasOne(t => t.Address).WithMany().HasForeignKey(v => v.AddressId);
        modelBuilder.Entity<ClientDbModel>().HasMany(t => t.Properties).WithOne(v => v.Client).HasForeignKey(property => property.clientId);

        modelBuilder.Entity<AddressDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<AddressDbModel>().ToTable("Address");
        modelBuilder.Entity<AddressDbModel>().Property(t => t.Number).HasMaxLength(50);
        modelBuilder.Entity<AddressDbModel>().Property(t => t.StreetName).HasMaxLength(50);
        modelBuilder.Entity<AddressDbModel>().Property(t => t.City).HasMaxLength(50);
        modelBuilder.Entity<AddressDbModel>().Property(t => t.County).HasMaxLength(50);
        modelBuilder.Entity<AddressDbModel>().Property(t => t.PostCode).HasMaxLength(50);
        modelBuilder.Entity<AddressDbModel>().Property(t => t.Country).HasMaxLength(50);

        modelBuilder.Entity<PropertyDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<PropertyDbModel>().ToTable("Property");
        modelBuilder.Entity<PropertyDbModel>().Property(t => t.Price).HasMaxLength(50);
        modelBuilder.Entity<PropertyDbModel>().Property(t => t.Description).HasMaxLength(1000);
        modelBuilder.Entity<PropertyDbModel>().HasOne(t => t.Address).WithMany().HasForeignKey(v => v.AddressId);

        modelBuilder.Entity<RoomDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<RoomDbModel>().ToTable("Room");
        modelBuilder.Entity<RoomDbModel>().Property(t => t.RoomType).HasMaxLength(50);
        modelBuilder.Entity<RoomDbModel>().Property(t => t.Description).HasMaxLength(1000);

        modelBuilder.Entity<CaseDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<CaseDbModel>().ToTable("Case");
        modelBuilder.Entity<CaseDbModel>().HasOne(t => t.Client).WithMany().HasForeignKey(v => v.ClientId);
        modelBuilder.Entity<CaseDbModel>().Property(t => t.Reference).HasMaxLength(50);
        modelBuilder.Entity<CaseDbModel>().HasOne(t => t.Property).WithMany().HasForeignKey(v => v.PropertyId);
        modelBuilder.Entity<CaseDbModel>().HasOne(t => t.CaseType).WithMany().HasForeignKey(v => v.CaseTypeId);
        modelBuilder.Entity<CaseDbModel>().HasOne(t => t.CaseStatus).WithMany().HasForeignKey(v => v.CaseStatusId);

        modelBuilder.Entity<CaseTypeDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<CaseTypeDbModel>().ToTable("CaseType");
        modelBuilder.Entity<CaseTypeDbModel>().Property(t => t.Type).HasMaxLength(100);

        modelBuilder.Entity<CaseStatusDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<CaseStatusDbModel>().ToTable("CaseStatus");
        modelBuilder.Entity<CaseStatusDbModel>().Property(t => t.Status).HasMaxLength(100);

        modelBuilder.Entity<UserDbModel>().HasKey(t => t.Id);
        modelBuilder.Entity<UserDbModel>().ToTable("User");
        modelBuilder.Entity<UserDbModel>().Property(t => t.UniqueId).HasMaxLength(500);
        modelBuilder.Entity<UserDbModel>().Property(t => t.Title).HasMaxLength(500);
        modelBuilder.Entity<UserDbModel>().Property(t => t.FirstName).HasMaxLength(500);
        modelBuilder.Entity<UserDbModel>().Property(t => t.LastName).HasMaxLength(500);
        modelBuilder.Entity<UserDbModel>().Property(t => t.UserName).HasMaxLength(500);
        modelBuilder.Entity<UserDbModel>().Property(t => t.Email).HasMaxLength(50);
        modelBuilder.Entity<UserDbModel>().Property(t => t.Phone).HasMaxLength(20);
        modelBuilder.Entity<UserDbModel>().Property(t => t.Picture).HasMaxLength(1000);
        modelBuilder.Entity<UserDbModel>().Property(t => t.IsBlocked).HasDefaultValue(false);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

}

