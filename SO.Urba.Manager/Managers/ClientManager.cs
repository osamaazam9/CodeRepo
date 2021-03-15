using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Extensions;
using SO.Urba.Models.ValueObjects; 
using SO.Urba.DbContexts;
using SO.Urba.Managers.Base;
using SO.Utility.Classes; 
using SO.Utility.Models.ViewModels;
using SO.Utility;
using SO.Utility.Helpers;
using SO.Utility.Extensions;
using System.Data.Entity.SqlServer;

namespace  SO.Urba.Managers 
{
    public class ClientManager : ClientManagerBase
    {
	 
        public ClientManager()
        {
		 
        }

        /// <summary>
        /// Find Client Full name - need  for client drop down
        /// </summary>
        public string getContactName(int? id = null)
        {
            using (var db = new MainDb())
            {
                var query = db.clients
                            .Include(i => i.contactInfo)
                            .FirstOrDefault(p => p.clientId == id);

                var fullName = query.contactInfo.fullname;

                if (id == null)
                    return "All Clients";
                else
                    return fullName;
            }
        }

        //
        public SearchFilterVm clientListExport(SearchFilterVm input)
        {
            int keywordInt = 0;
            int.TryParse(input.keyword, out keywordInt);
            
            using (var db = new MainDb())
            {
                var query = db.clients
                    .Include(i => i.contactInfo)
                    //.Include(o => o.clientOrganizationLookupses)
                    .Include(a => a.clientOrganizationLookupses.Select(c => c.organization))
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      &&
                                      (e.contactInfo!=null && (
                                      string.IsNullOrEmpty(input.keyword)==true
                                 || e.contactInfo.firstName.Contains(input.keyword)
                                 || e.contactInfo.lastName.Contains(input.keyword)

                                      || e.contactInfo.address.Contains(input.keyword)
                                 || e.contactInfo.city.Contains(input.keyword)
                                 || e.contactInfo.state.Contains(input.keyword)
                                 || e.contactInfo.homePhone.Contains(input.keyword)
                                 || e.contactInfo.workPhone.Contains(input.keyword)

                                       || (keywordInt > 0 && e.clientId == keywordInt)
                                      )));

                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object>();
  
                    items.Add(
                                new
                                {
                                    h1 = "ID",
                                    h2 = "Client Name",
                                    h3 = "Address",
                                    h4 = "Work Phone",
                                    h5 = "Fee Has Paid",
                                    h6 = "Organization"
                                });

                    var item1 = query.Select(i =>
                                new
                                {
                                    ID = i.clientId,
                                    Client_Name = i.contactInfo.firstName + " " + i.contactInfo.lastName,
                                    Address = i.contactInfo.address + ", " + i.contactInfo.city
                                      + " " + i.contactInfo.state + " " + i.contactInfo.zip,
                                    Work_Phone = i.contactInfo.workPhone,
                                    HasPaidFee = (i.hasPaidFee == null ? "N/A" : (i.hasPaidFee == true ? "Yes" : "No")),
                                    Organization = i.clientOrganizationLookupses.Select(c=>c.organization.name).ToList() 
                                }).ToList<dynamic>();

                    foreach (var j in item1)
                    {
                        items.Add((object)
                                    new
                                    {
                                        ID = j.ID,
                                        Client_Name = j.Client_Name,
                                        Address = j.Address,
                                        Work_Phone = j.Work_Phone,
                                        HasPaidFee = j.HasPaidFee,
                                        Organization = string.Join(", ", j.Organization)
                                    });
                    }

                    input.result = items;
                }

                return input;
            }
        }
        


    }
}

