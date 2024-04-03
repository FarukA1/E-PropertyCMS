using System;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Contexts
{
	public interface ICoreContext
    {
        DbSet<ClientDbModel> Client { get; set; }
        DbSet<AddressDbModel> Address { get; set; }
        DbSet<PropertyDbModel> Property { get; set; }
        DbSet<RoomDbModel> Room { get; set; }
        DbSet<CaseDbModel> Case { get; set; }
        DbSet<CaseTypeDbModel> CaseType { get; set; }
        DbSet<UserDbModel> User { get; set; }


        Task<int> SaveChangesAsync();
    }
}

