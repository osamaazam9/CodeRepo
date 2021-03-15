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
using SO.Urba.Manager.Models.ViewModels;

namespace SO.Urba.Managers
{
    public class CompanyCategoryTypeManager : CompanyCategoryTypeManagerBase
    {

        public CompanyCategoryTypeManager()
        {

        }

        //
        public List<CompanyCategoryTypeVo> getCompanyCategories(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                List<CompanyCategoryTypeVo> listContractorCategories =
                    (from a in db.companyCategoryTypes
                     select a)
                      .OrderBy(x => x.name)
                      .Where(gg => isActive == null || gg.isActive == isActive)
                      .ToList();

                return listContractorCategories;
            }
        }


    }
}

