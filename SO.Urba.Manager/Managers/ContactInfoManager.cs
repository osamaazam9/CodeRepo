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
using SO.Urba.Models.ViewModels;

namespace  SO.Urba.Managers 
{ 
    public class ContactInfoManager : ContactInfoManagerBase
    {
	 
        public ContactInfoManager()
        {
		 
        }

        //public ContactInfoSearchFilterVm search(ContactInfoSearchFilterVm input)
        //{

        //    using (var db = new MainDb())
        //    {
        //        var query = db.contactInfos
        //                     .OrderByDescending(b => b.created)
        //                     .Where(e => (input.isActive == null || e.isActive == input.isActive)
        //                              && ((e.firstName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
        //                                || (e.lastName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword)))
        //                            );

        //        if (input.paging != null)
        //        {
        //            input.paging.totalCount = query.Count();
        //            query = query
        //                    .Skip(input.paging.skip)
        //                    .Take(input.paging.rowCount);

        //        }

        //        input.result = query.ToList<object>();

        //        return input;
        //    }
        //}


        public ContactInfoVm insert(ContactInfoVm input)
        {
            using (var db = new MainDb())
            {           
                var company = db.companies.FirstOrDefault(c => c.companyId == input.companyId);
                db.contactInfos.Add(input.contactInfo);
                db.SaveChanges();

                if (company != null)
                    company.contactInfoId = input.contactInfo.contactInfoId;

                db.SaveChanges();

                return input;
            }
        }

        // added this because ContactInfoVo resulted in error
        //public ContactInfoVo insert(ContactInfoVo input, int companyId)
        //{
        //    using (var db = new MainDb())
        //    {
        //        var company = db.companies.FirstOrDefault(c => c.companyId == companyId);
        //        db.contactInfos.Add(input);
        //        db.SaveChanges();

        //        if (company != null)
        //            company.contactInfoId = input.contactInfoId;

        //        db.SaveChanges();

        //        return input;
        //    }
        //}
  

    }
}

