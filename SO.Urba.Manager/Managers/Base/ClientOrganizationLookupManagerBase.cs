using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks; 
using System.Data.Entity;
using EntityFramework.Extensions;
using SO.Urba.Models.ValueObjects; 
using SO.Urba.DbContexts;
using SO.Utility.Models.ViewModels;
using SO.Utility;
using SO.Utility.Helpers;
using SO.Utility.Extensions;
 

namespace  SO.Urba.Managers.Base
{
    public class ClientOrganizationLookupManagerBase
    {

        public ClientOrganizationLookupManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find ClientOrganizationLookup matching the clientId-organizationId (primary key)
        /// </summary>
        public ClientOrganizationLookupVo get(Guid clientOrganizationLookupId)
        {
            using (var db = new MainDb())
            {
                var res = db.clientOrganizationLookups
                            .FirstOrDefault(p => p.clientOrganizationLookupId == clientOrganizationLookupId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public ClientOrganizationLookupVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.clientOrganizationLookups
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.clientOrganizationLookups
                             .Include(c=>c.client)
                             .Include(d=>d.organization)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.organization.name.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
                                    );
             
			  if (input.paging != null) { 
					 input.paging.totalCount = query.Count();
					 query =query
                             .Skip(input.paging.skip)
                             .Take(input.paging.rowCount);
                            
				 }
                
                input.result = query.ToList<object>();
				 
                return input;
            }
        }

        //
        public List<ClientOrganizationLookupVo> getAll(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                var list = db.clientOrganizationLookups
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(Guid clientOrganizationLookupId)
        {
            using (var db = new MainDb())
            {
                var res = db.clientOrganizationLookups
                     .Where(e => e.clientOrganizationLookupId == clientOrganizationLookupId)
                     .Delete();
                return true;
            } 
        }

        public ClientOrganizationLookupVo update(ClientOrganizationLookupVo input, Guid? clientOrganizationLookupId = null)
        {
        
            using (var db = new MainDb())
            {

                if (clientOrganizationLookupId == null)
                    clientOrganizationLookupId = input.clientOrganizationLookupId;

                var res = db.clientOrganizationLookups.FirstOrDefault(e => e.clientOrganizationLookupId == clientOrganizationLookupId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public ClientOrganizationLookupVo insert(ClientOrganizationLookupVo input)
        {
            using (var db = new MainDb())
            {

                db.clientOrganizationLookups.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.clientOrganizationLookups.Count();
            }
        }
		 
        
    }
}

