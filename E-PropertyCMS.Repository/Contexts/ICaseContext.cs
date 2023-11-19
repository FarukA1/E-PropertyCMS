using System;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Contexts
{
	public interface ICaseContext
	{
        DbSet<CaseDbModel> Case { get; set; }
        DbSet<CaseTypeDbModel> CaseType { get; set; }
        DbSet<CaseStatusDbModel> CaseStatus { get; set; }
    }
}

