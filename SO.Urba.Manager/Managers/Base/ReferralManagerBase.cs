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


namespace SO.Urba.Managers.Base
{
    public class ReferralManagerBase
    {

        public ReferralManagerBase()
        {

        }

        /// <summary>
        /// Find Referral matching the referralId (primary key)
        /// </summary>
        public ReferralVo get(int referralId)
        {
            using (var db = new MainDb())
            {
                var res = db.referrals
                            .Include(a => a.surveyAnswerses)
                            .Include(q => q.surveyAnswerses.Select(c => c.questionType))
                            .FirstOrDefault(p => p.referralId == referralId);

                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public ReferralVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.referrals
                    .Include(a => a.surveyAnswerses)
                            .FirstOrDefault();

                return res;
            }
        }

        public SearchFilterVm search(SearchFilterVm input)
        {

            using (var db = new MainDb())
            {
                var query = db.referrals
                    .Include(a => a.surveyAnswerses)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                    // && (e.accepted.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
                                    );

                if (input.paging != null)
                {
                    input.paging.totalCount = query.Count();
                    query = query
                            .Skip(input.paging.skip)
                            .Take(input.paging.rowCount);

                }

                input.result = query.ToList<object>();

                return input;
            }
        }

        //
        public List<ReferralVo> getAll(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                var list = db.referrals
                    .Include(a => a.surveyAnswerses)
                             .Where(e => isActive == null || e.isActive == isActive)
                             .ToList();

                return list;
            }
        }


        public bool delete(int referralId)
        {
            using (var db = new MainDb())
            {
                var res = db.referrals
                     .Where(e => e.referralId == referralId)
                     .Delete();
                return true;
            }
        }

        public ReferralVo update(ReferralVo input, int? referralId = null)
        {

            using (var db = new MainDb())
            {

                if (referralId == null)
                    referralId = input.referralId;

                var res = db.referrals.FirstOrDefault(e => e.referralId == referralId);

                if (res == null) return null;

                input.created = res.created;
                // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);


                db.SaveChanges();
                return res;

            }
        }

        public ReferralVo insert(ReferralVo input)
        {
            using (var db = new MainDb())
            {

                db.referrals.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.referrals.Count();
            }
        }


    }
}

