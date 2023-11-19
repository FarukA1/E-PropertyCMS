using System;
namespace E_PropertyCMS.Repository.Models.Mapping
{
	public interface IAddFromDomain<in TDomainModel>
	{
		void AddFromDomain(TDomainModel domain);
	}
}

