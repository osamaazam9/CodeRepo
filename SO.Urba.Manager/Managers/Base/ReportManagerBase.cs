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
using SO.Urba.Models.ViewModels;
 

namespace  SO.Urba.Managers.Base
{
    public class ReportManagerBase
    {

        public ReportManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find Company matching the companyId (primary key)
        /// </summary>


        /// <summary>
        /// Get First Item
        /// </summary>


        //public ReportSearchFilterVm search(ReportSearchFilterVm input)
        //{
		 
        //    using (var db = new MainDb())
        //    {
        //        var query = db.companies
        //            .Include(i => i.contactInfo)
        //            .Include(c => c.companyCategoryLookupses)
        //                     .OrderByDescending(b => b.created)
        //                     .Where(e => (input.isActive == null || e.isActive == input.isActive)
        //                              && (e.companyName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
        //                            );
             
        //      if (input.paging != null) { 
        //             input.paging.totalCount = query.Count();
        //             query =query
        //                     .Skip(input.paging.skip)
        //                     .Take(input.paging.rowCount);
                            
        //         }
                
        //        input.result = query.ToList<object>();
				 
        //        return input;
        //    }
        //}
		 
        
    }
}

