using System;
using E_PropertyCMS.Core.Services;

namespace E_PropertyCMS.Repository.Contexts
{
	public interface ICoreContext :
        IUnitOfWork,
        IClientContext,
        IPropertyContext,
        ICaseContext
    {
	}
}

