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
using System.Data.Entity.SqlServer;

namespace SO.Urba.Managers
{
    public class CompanyManager : CompanyManagerBase
    {

        public CompanyManager()
        {

        }

        public CompanySearchFilterVm search(CompanySearchFilterVm input)
        {

            using (var db = new MainDb())
            {
                var query = db.companies
                    .Include(i => i.contactInfo)
                    .Include(s => s.referralses)
                    .Include("referralses.surveyAnswerses")
                    .Include(c => c.companyCategoryLookupses)
                    .Include(k => k.companyCategoryLookupses.Select(v => v.companyCategoryType))
                    .OrderByDescending(b => b.created)
                    .Where(e => (input.isActive == null || e.isActive == input.isActive)
                            && (e.companyName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
                            && (e.companyCategoryLookupses.Any(b => b.companyCategoryTypeId == input.companyCategoryTypeId) || input.companyCategoryTypeId == null)
                     );

                if (input.paging != null)
                {
                    input.paging.totalCount = query.Count();
                    query = query
                            .Skip(input.paging.skip)
                            .Take(input.paging.rowCount);

                }

                input.result = query
                    .ToList()
                    .Select(c =>
                    {
                        var surveyAnswers = c.referralses.SelectMany(r => r.surveyAnswerses);
                        var score = (surveyAnswers.Count(u => u.answer != null) == 0 ? null : (decimal?)surveyAnswers.Select(u => u.answer).Sum() / surveyAnswers.Count(u => u.answer != null)) * 20;
                        c.score = (score != null) ? (decimal?)Math.Round(score.GetValueOrDefault(), 2) : null;
                        c.scoreCount = surveyAnswers.Select(u => u.referralId).Distinct().Count();
                        return c;
                    })
                    .ToList<object>();

                return input;
            }
        }

        public CompanySearchFilterVm contactsByProvider(CompanySearchFilterVm input)
        {

            using (var db = new MainDb())
            {
                var query = db.companies
                    .Include(i => i.contactInfo)
                    .Include(c => c.companyCategoryLookupses)
                    .Include(k => k.companyCategoryLookupses.Select(v => v.companyCategoryType))
                    .Include(j => j.referralses.Select(m => m.surveyAnswerses))
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.companyName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
                                      && (e.companyCategoryLookupses.Any(b => b.companyCategoryTypeId == input.companyCategoryTypeId) || input.companyCategoryTypeId == null)
                                    );

                input.result = query.ToList<object>();
                if (input.result != null)
                {
                    var items = query.Select(i =>
                                new
                                {
                                    Id = i.companyId,
                                    CompanyName = i.companyName,
                                    FirstName = i.contactInfo.firstName,
                                    LastName = i.contactInfo.lastName,
                                    WorkPhone = i.contactInfo.workPhone,
                                    License = i.license,
                                    Bonded = i.bonded,
                                    AgreementSigned = SqlFunctions.DateName("day", i.agreementSigned).Trim() + "/" +
                                               SqlFunctions.StringConvert((double)i.agreementSigned.Value.Month).TrimStart() + "/" +
                                               SqlFunctions.DateName("year", i.agreementSigned),

                                    //score_up = (i.referralses.Any(r => r.companyId == i.companyId)
                                    //    ? i.referralses.Sum(x => x.surveyAnswerses.Sum(r => r.answer)) * 20
                                    //    : null),
                                    //score_dn = i.referralses.Sum(t => t.surveyAnswerses.Count()),

                                    Score = (i.referralses.Any(r => r.companyId == i.companyId)
                                        ? (decimal?)i.referralses.Sum(x => x.surveyAnswerses.Sum(r => r.answer)) * 20 / i.referralses.Sum(t => t.surveyAnswerses.Count())
                                        : null),

                                    ScoreCount = (i.referralses.Any(r => r.companyId == i.companyId)
                                        ? i.referralses.Where(e => e.surveyAnswerses.Any(b => b.referralId != null)).Count()
                                        : 0)
                                }).ToList<object>();

                    input.result = items;
                }

                return input;
            }
        }


        //
        public List<CompanyVo> getAllOfGivenCategory(int companyCategoryTypeId, bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                List<CompanyVo> companyList = new List<CompanyVo>();

                var query = (
                        from _company in db.companies
                        join _contactInfo in db.contactInfos on _company.contactInfoId equals _contactInfo.contactInfoId
                        from _companyCategoryLookup in db.companyCategoryLookups.Where(cct => _company.companyId == cct.companyId).DefaultIfEmpty()
                        from _Referral in db.referrals.Where(_Referral => _company.companyId == _Referral.companyId).DefaultIfEmpty()
                        from _surveyAnswers in db.surveyAnswers.Where(_surveyAnswers => _surveyAnswers.referralId == _Referral.referralId).DefaultIfEmpty()
                        where _companyCategoryLookup.companyCategoryTypeId == companyCategoryTypeId
                        group new
                        {
                            _company.companyId,
                            _company.companyName,
                            _contactInfo.firstName,
                            _contactInfo.lastName,
                            _contactInfo.workPhone,
                            _company.license,
                            _company.bonded,
                            _company.agreementSigned,
                            _surveyAnswers
                        }
                        by
                           new
                           {
                               _company.companyId,
                               _company.companyName,
                               _contactInfo.firstName,
                               _contactInfo.lastName,
                               _contactInfo.workPhone,
                               _company.license,
                               _company.bonded,
                               _company.agreementSigned
                           }
                            into g
                        select new
                        {
                            companyId = g.Key.companyId,
                            companyName = g.Key.companyName,
                            firstName = g.Key.firstName,
                            lastName = g.Key.lastName,
                            workPhone = g.Key.workPhone,
                            license = g.Key.license,
                            bonded = g.Key.bonded,
                            agreementSigned = g.Key.agreementSigned,
                            a_score = ((decimal?)g.Sum(t => t._surveyAnswers.answer) / g.Count(u => u._surveyAnswers.answer != null)) * 20,
                            score = ((decimal?)g.Select(u => u._surveyAnswers.answer).Sum() / g.Count(u => u._surveyAnswers.answer != null)) * 20,
                            scoreCount = g.Select(u => u._surveyAnswers.referralId).Distinct().Count(z => z != null)
                        }
                );

                var items = query.ToList();
                foreach (var obj in items)
                {
                    CompanyVo c = new CompanyVo();
                    c.contactInfo = new ContactInfoVo();
                    c.companyId = obj.companyId;
                    c.companyName = obj.companyName;
                    c.contactInfo.firstName = obj.firstName;
                    c.contactInfo.lastName = obj.lastName;
                    c.contactInfo.workPhone = obj.workPhone;
                    c.score = (obj.score != null) ? (decimal?)Math.Round(obj.score.GetValueOrDefault(), 2) : null;
                    c.scoreCount = obj.scoreCount;
                    companyList.Add(c);
                }

                return companyList;
            }
        }

    }
}

