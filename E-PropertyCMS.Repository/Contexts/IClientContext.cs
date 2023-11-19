using System;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Contexts
{
	public interface IClientContext
	{
        DbSet<ClientDbModel> Client { get; set; }
        DbSet<AddressDbModel> Address { get; set; }
    }
}

