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

namespace  SO.Urba.Managers 
{ 
    public class ReferralManager : ReferralManagerBase
    {
	 
        public ReferralManager()
        {
		 
        }

        public static decimal? addAllFinalPayments(int? clientID)
        {
            decimal? totalAmountForFinalPayments = 0;
            List<ReferralVo> rList = new List<ReferralVo>();
            using (var db = new MainDb())
            {
                rList = db.referrals
                          .Include(q => q.client)
                          .Where(e => (e.clientId == clientID))
                          .ToList();
            }
            foreach (ReferralVo rVo in rList)
            {
                DateTime referralDate = (DateTime)rVo.referralDate;
                if ((referralDate.Year == DateTime.Now.Year) && (rVo.finalQuote != null)) 
                {
                    totalAmountForFinalPayments += rVo.finalQuote;
                }

            }
            return totalAmountForFinalPayments;
        }
        
    }
}

