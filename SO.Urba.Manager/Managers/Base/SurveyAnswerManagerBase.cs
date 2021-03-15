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
    public class SurveyAnswerManagerBase
    {

        public SurveyAnswerManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find SurveyAnswer matching the surveyAnswerId (primary key)
        /// </summary>
        public SurveyAnswerVo get(Guid  surveyAnswerId )
        {
            using (var db = new MainDb())
            {
                var res = db.surveyAnswers
                    .Include(a => a.questionType)
                            .FirstOrDefault(p => p.surveyAnswerId == surveyAnswerId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public SurveyAnswerVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.surveyAnswers
                    .Include(a => a.questionType)
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.surveyAnswers
                    .Include(a => a.questionType)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                     // && (e.referralId.ToString().Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<SurveyAnswerVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.surveyAnswers
                    .Include(a => a.questionType)
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(Guid surveyAnswerId)
        {
            using (var db = new MainDb())
            {
                var res = db.surveyAnswers
                     .Where(e => e.surveyAnswerId == surveyAnswerId)
                     .Delete();
                return true;
            } 
        } 

        public SurveyAnswerVo update(SurveyAnswerVo input, Guid? surveyAnswerId= null)
        {
        
            using (var db = new MainDb())
            {

                if (surveyAnswerId == null)
                    surveyAnswerId = input.surveyAnswerId; 

                var res = db.surveyAnswers.FirstOrDefault(e => e.surveyAnswerId == surveyAnswerId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public SurveyAnswerVo insert(SurveyAnswerVo input)
        {
            using (var db = new MainDb())
            {
                
                db.surveyAnswers.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.surveyAnswers.Count();
            }
        }
		 
        
    }
}

