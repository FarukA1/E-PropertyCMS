using System;
namespace E_PropertyCMS.Domain.Model
{
	public class Dashboard
	{
        public int NumberofClients { get; set; }
        public int NumberofCases { get; set; }
        public int NumberofProperties { get; set; }

        public List<Client> Clients { get; set; }
        public List<Case> Cases { get; set; }
    }
}