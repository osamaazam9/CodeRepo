using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Extensions;
using SO.Urba.Manager.Models.ViewModels;
using SO.Urba.Models.ValueObjects;
using SO.Urba.DbContexts;
using SO.Urba.Managers.Base;
using SO.Utility.Classes;
using SO.Utility.Models.ViewModels;
using SO.Utility;
using SO.Utility.Helpers;
using SO.Utility.Extensions;

namespace SO.Urba.Managers
{
    public class ReferralRecordManager
    {
        public ReferralRecordManager()
        {
        }

        /// <summary>
        ///  Caterers, iPayment, 01/30/2014
        ///  Caterers, iPayment Inc, 01/30/2014
        ///  Caterers, Jerry's, 01/30/2014
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="referralFilter"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<int> addTrippleReferral(int clientId, ReferralVo referralFilter, bool? isActive = true)
        {
            List<int> referrals = new List<int>();

            using (var db = new MainDb())
            {
                var result =
                    (from c in db.companies
                     join ccl in db.companyCategoryLookups on c.companyId equals ccl.companyId
                     where ccl.companyCategoryTypeId == referralFilter.companyCategoryTypeId
                     select new { Company = c, Count = c.referralses.Any() ? c.referralses.Count() : 0 })
                    .OrderBy(c => c.Count)
                    .Take(3)
                    .Select(c => new { c.Company.companyId, c.Count })
                    .ToList();

                foreach (var rfData in result)
                {
                    ReferralVo rf = new ReferralVo();
                    rf.referralId = 0; //referralId is 0 because we are creating new ones
                    rf.clientId = clientId;
                    rf.companyId = rfData.companyId;
                    rf.companyCategoryTypeId = referralFilter.companyCategoryTypeId; // or rfData.companyCategoryTypeId
                    rf.referralDate = referralFilter.referralDate;
                    rf.quote = referralFilter.quote;
                    rf.accepted = referralFilter.accepted;
                    rf.finalQuote = referralFilter.finalQuote;
                    rf.finishDate = referralFilter.finishDate;
                    rf.description = referralFilter.description;
                    addNewOrUpdateReferral(rf);

                    referrals.Add(rf.referralId);
                }

                return referrals;
            }
        }

        public void addNewOrUpdateReferral(ReferralVo newReferral)
        {
            using (var db = new MainDb())
            {
                if (newReferral.referralId == 0) // add new
                {
                    var res = db.referrals.Add(newReferral);
                    db.SaveChanges();
                }
                else // update existing .... TODO: use this or remove the else block and rename the function
                {
                    return;
                }
            }
        }



        public List<ReferralVo> getReferralsOfMember(int? clientId = null, bool? isActive = true, ProviderReferralHistoryFilter providerReferralHistoryFilter = ProviderReferralHistoryFilter.All)
        {
            using (var db = new MainDb())
            {
                if (clientId != null)
                {
                    var result = db.referrals
                        .Include(c => c.client)
                        .Include(cct => cct.companyCategoryType)
                        .Include(c => c.company)
                        .Include(a => a.surveyAnswerses)
                        .Where(e =>
                            (clientId == null || e.client.clientId == clientId)
                            && (providerReferralHistoryFilter == ProviderReferralHistoryFilter.All || e.accepted))
                        .OrderByDescending(rd => rd.created)
                        .ToList();
                    return result;
                }
                return null;
            }
        }

        public List<ReferralVo> getReferralsOfMember2(int? clientId = null, bool? isActive = true)
        {
            List<ReferralVo> listReferralsOfClient = new List<ReferralVo>();
            if (clientId != null)
            {
                using (var db = new MainDb())
                {
                    var result = (
                        from r in db.referrals
                        join m in db.clients on r.clientId equals m.clientId
                        join c in db.companies on r.companyId equals c.companyId
                        join cct in db.companyCategoryTypes on r.companyCategoryTypeId equals cct.companyCategoryTypeId // into g
                        where ((clientId == null || m.clientId == clientId)
                                && (isActive == null || r.isActive == isActive)
                                && (isActive == null || m.isActive == isActive)
                                && (isActive == null || c.isActive == isActive)
                                && (isActive == null || cct.isActive == isActive)
                                )
                        select r
                    );

                    listReferralsOfClient = result.ToList();
                }
            }
            return listReferralsOfClient;
        }

        public List<ReferralVo> getReferralsOfProvider(int? companyId = null, bool? isActive = true, ProviderReferralHistoryFilter providerReferralHistoryFilter = ProviderReferralHistoryFilter.All)
        {
            using (var db = new MainDb())
            {
                if (companyId != null)
                {
                    var result = db.referrals
                        .Include(c => c.client)
                        .Include(c => c.client.contactInfo)
                        .Include(c => c.company)
                        .Where(e =>
                            (companyId == null || e.companyId == companyId)
                            && (providerReferralHistoryFilter == ProviderReferralHistoryFilter.All || e.accepted))

                        .OrderByDescending(rd => rd.created)
                        .ToList();

                    return result;
                }
                return null;
            }
        }

        public int getTrippeRefeerral(int? clientId = null)
        {
            return 0;
        }
    }
}

