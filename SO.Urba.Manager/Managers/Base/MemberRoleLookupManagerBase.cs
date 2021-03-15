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
    public class MemberRoleLookupManagerBase
    {

        public MemberRoleLookupManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find MemberRoleLookup matching the memberRoleId (primary key)
        /// </summary>
        public MemberRoleLookupVo get(Guid  memberRoleId )
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleLookups
                            .Include(r => r.memberRoleType)
                            .FirstOrDefault(p => p.memberRoleId == memberRoleId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public MemberRoleLookupVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleLookups
                     .Include(r => r.memberRoleType)
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
		 
            using (var db = new MainDb())
            {
                var query = db.memberRoleLookups
                     .Include(r => r.memberRoleType)
                     
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.memberRoleTypeId.ToString().Contains(input.keyword) || string.IsNullOrEmpty(input.keyword))
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
        public List<MemberRoleLookupVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.memberRoleLookups
                    .Include(r => r.memberRoleType)
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(Guid memberRoleId)
        {
            using (var db = new MainDb())
            {
                var res = db.memberRoleLookups
                     .Where(e => e.memberRoleId == memberRoleId)
                     .Delete();
                return true;
            } 
        } 

        public MemberRoleLookupVo update(MemberRoleLookupVo input, Guid? memberRoleId= null)
        {
        
            using (var db = new MainDb())
            {

                if (memberRoleId == null)
                    memberRoleId = input.memberRoleId; 

                var res = db.memberRoleLookups.FirstOrDefault(e => e.memberRoleId == memberRoleId);

                if (res == null) return null;

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public MemberRoleLookupVo insert(MemberRoleLookupVo input)
        {
            using (var db = new MainDb())
            {
                
                db.memberRoleLookups.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.memberRoleLookups.Count();
            }
        }
		 
        
    }
}

