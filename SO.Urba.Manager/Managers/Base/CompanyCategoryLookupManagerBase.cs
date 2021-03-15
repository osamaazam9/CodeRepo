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
    public class CompanyCategoryLookupManagerBase
    {

        public CompanyCategoryLookupManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find CompanyCategoryLookup matching the companyCategoryId (primary key)
        /// </summary>
        public CompanyCategoryLookupVo get(Guid  companyCategoryId )
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryLookups
                            .FirstOrDefault(p => p.companyCategoryId == companyCategoryId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public CompanyCategoryLookupVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryLookups
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.companyCategoryLookups
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.companyId.ToString().Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<CompanyCategoryLookupVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.companyCategoryLookups
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(Guid companyCategoryId)
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryLookups
                     .Where(e => e.companyCategoryId == companyCategoryId)
                     .Delete();
                return true;
            } 
        } 

        public CompanyCategoryLookupVo update(CompanyCategoryLookupVo input, Guid? companyCategoryId= null)
        {
        
            using (var db = new MainDb())
            {

                if (companyCategoryId == null)
                    companyCategoryId = input.companyCategoryId; 

                var res = db.companyCategoryLookups.FirstOrDefault(e => e.companyCategoryId == companyCategoryId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public CompanyCategoryLookupVo insert(CompanyCategoryLookupVo input)
        {
            using (var db = new MainDb())
            {
                
                db.companyCategoryLookups.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.companyCategoryLookups.Count();
            }
        }
		 
        
    }
}

