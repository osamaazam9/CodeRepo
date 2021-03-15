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
    public class CompanyManagerBase
    {

        public CompanyManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find Company matching the companyId (primary key)
        /// </summary>
        public CompanyVo get(int  companyId )
        {
            using (var db = new MainDb())
            {
                var res = db.companies
                    .Include(i => i.contactInfo)
                    .Include(r => r.referralses)
                    .Include("referralses.surveyAnswerses")
                    .Include(c => c.companyCategoryLookupses)
                    .Where(p => p.companyId == companyId)
                    .ToList()
                    .Select(c =>
                    {
                        var surveyAnswers = c.referralses.SelectMany(r => r.surveyAnswerses);
                        var score = (surveyAnswers.Count(u => u.answer != null) == 0 ? null : (decimal?)surveyAnswers.Select(u => u.answer).Sum() / surveyAnswers.Count(u => u.answer != null)) * 20;
                        c.score = (score != null) ? (decimal?)Math.Round(score.GetValueOrDefault(), 2) : null;
                        c.scoreCount = surveyAnswers.Select(u => u.referralId).Distinct().Count();
                        return c;
                    })
                    .FirstOrDefault();

                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public CompanyVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.companies
                    .Include(i => i.contactInfo)
                    .Include(c => c.companyCategoryLookupses)
                            .FirstOrDefault();
               
                return res;
            }
        }

        public CompanySearchFilterVm search(CompanySearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.companies
                    .Include(i => i.contactInfo)
                    .Include(c => c.companyCategoryLookupses)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.companyName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<CompanyVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.companies
                    .Include(i => i.contactInfo)
                    .Include(c => c.companyCategoryLookupses)
                             .Where(e => isActive==null || e.isActive == isActive )
                             .OrderBy(i => i.companyName).ToList();

                return list;
            }
        }

        public bool delete(int companyId)
        {
            using (var db = new MainDb())
            {
                var res = db.companies
                     .Where(e => e.companyId == companyId)
                     .Delete();
                return true;
            } 
        } 

        public CompanyVo update(CompanyVo input, int? companyId= null)
        {
        
            using (var db = new MainDb())
            {

                if (companyId == null)
                    companyId = input.companyId;

                var res = db.companies.Include(i => i.contactInfo).FirstOrDefault(e => e.companyId == companyId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public CompanyVo insert(CompanyVo input)
        {
            using (var db = new MainDb())
            {
                
                db.companies.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.companies.Count();
            }
        }
		 
        
    }
}

