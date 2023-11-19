using System;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Contexts
{
	public interface IPropertyContext
	{
        DbSet<PropertyDbModel> Property { get; set; }
        DbSet<RoomDbModel> Room { get; set; }
    }
}

