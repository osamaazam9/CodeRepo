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
    public class MemberRoleTypeManagerBase
    {

        public MemberRoleTypeManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find MemberRoleType matching the memberRoleTypeId (primary key)
        /// </summary>
        public MemberRoleTypeVo get(int  memberRoleTypeId )
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleTypes
                            .FirstOrDefault(p => p.memberRoleTypeId == memberRoleTypeId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public MemberRoleTypeVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleTypes
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.memberRoleTypes
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.name.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<MemberRoleTypeVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.memberRoleTypes
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(int memberRoleTypeId)
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleTypes
                     .Where(e => e.memberRoleTypeId == memberRoleTypeId)
                     .Delete();
                return true;
            } 
        } 

        public MemberRoleTypeVo update(MemberRoleTypeVo input, int? memberRoleTypeId= null)
        {
        
            using (var db = new MainDb())
            {

                if (memberRoleTypeId == null)
                    memberRoleTypeId = input.memberRoleTypeId; 

                var res = db.memberRoleTypes.FirstOrDefault(e => e.memberRoleTypeId == memberRoleTypeId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public MemberRoleTypeVo insert(MemberRoleTypeVo input)
        {
            using (var db = new MainDb())
            {
                
                db.memberRoleTypes.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.memberRoleTypes.Count();
            }
        }
		 
        
    }
}

