using System;
namespace E_PropertyCMS.Repository.Models.Mapping
{
	public interface IAddToDomain<out TDomainModel>
	{
		TDomainModel AddToDomain();
	}
}

