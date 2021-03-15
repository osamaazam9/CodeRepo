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
    public class QuestionTypeManagerBase
    {

        public QuestionTypeManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find QuestionType matching the questionTypeId (primary key)
        /// </summary>
        public QuestionTypeVo get(int  questionTypeId )
        {
            using (var db = new MainDb())
            {
                var res = db.questionTypes
                            .FirstOrDefault(p => p.questionTypeId == questionTypeId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public QuestionTypeVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.questionTypes
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.questionTypes
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.questionText.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<QuestionTypeVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.questionTypes
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(int questionTypeId)
        {
            using (var db = new MainDb())
            {
                var res = db.questionTypes
                     .Where(e => e.questionTypeId == questionTypeId)
                     .Delete();
                return true;
            } 
        } 

        public QuestionTypeVo update(QuestionTypeVo input, int? questionTypeId= null)
        {
        
            using (var db = new MainDb())
            {

                if (questionTypeId == null)
                    questionTypeId = input.questionTypeId; 

                var res = db.questionTypes.FirstOrDefault(e => e.questionTypeId == questionTypeId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public QuestionTypeVo insert(QuestionTypeVo input)
        {
            using (var db = new MainDb())
            {
                
                db.questionTypes.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.questionTypes.Count();
            }
        }
		 
        
    }
}

