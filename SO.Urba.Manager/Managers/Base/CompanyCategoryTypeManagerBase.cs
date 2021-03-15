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
    public class CompanyCategoryTypeManagerBase
    {

        public CompanyCategoryTypeManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find CompanyCategoryType matching the companyCategoryTypeId (primary key)
        /// </summary>
        public CompanyCategoryTypeVo get(int  companyCategoryTypeId )
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryTypes
                            .FirstOrDefault(p => p.companyCategoryTypeId == companyCategoryTypeId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public CompanyCategoryTypeVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryTypes
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.companyCategoryTypes
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.name.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<CompanyCategoryTypeVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.companyCategoryTypes
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(int companyCategoryTypeId)
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryTypes
                     .Where(e => e.companyCategoryTypeId == companyCategoryTypeId)
                     .Delete();
                return true;
            } 
        } 

        public CompanyCategoryTypeVo update(CompanyCategoryTypeVo input, int? companyCategoryTypeId= null)
        {
        
            using (var db = new MainDb())
            {

                if (companyCategoryTypeId == null)
                    companyCategoryTypeId = input.companyCategoryTypeId; 

                var res = db.companyCategoryTypes.FirstOrDefault(e => e.companyCategoryTypeId == companyCategoryTypeId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public CompanyCategoryTypeVo insert(CompanyCategoryTypeVo input)
        {
            using (var db = new MainDb())
            {
                
                db.companyCategoryTypes.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.companyCategoryTypes.Count();
            }
        }
		 
        
    }
}

