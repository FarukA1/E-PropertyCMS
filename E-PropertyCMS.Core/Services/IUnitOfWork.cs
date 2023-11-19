using System;
namespace E_PropertyCMS.Core.Services
{
	public interface IUnitOfWork
	{
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}

