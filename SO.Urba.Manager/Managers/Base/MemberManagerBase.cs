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
    public class MemberManagerBase
    {

        public MemberManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find Member matching the memberId (primary key)
        /// </summary>
        public MemberVo get(int  memberId )
        {
            using (var db = new MainDb())
            {
                var res = db.members
                                                 .Include(i => i.contactInfo)
                            .Include(a => a.memberRoleLookupses.Select(c => c.memberRoleType))
                            .FirstOrDefault(p => p.memberId == memberId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public MemberVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.members
                                                 .Include(i => i.contactInfo)
                            .Include(a => a.memberRoleLookupses)
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
            int keywordInt = 0;

            int.TryParse(input.keyword, out keywordInt);
            if (keywordInt < 0)
                keywordInt = 0;

            using (var db = new MainDb())
            {
                var query = db.members
                            
                             .Include(i => i.contactInfo)
                            .Include(a => a.memberRoleLookupses.Select(c => c.memberRoleType))
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.username.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword)
                                      || e.memberId == keywordInt
                                      || e.contactInfoId == keywordInt
                                      || e.contactInfo.address.Contains(input.keyword)
                                      || e.contactInfo.city.Contains(input.keyword)
                                      || e.contactInfo.firstName.Contains(input.keyword)
                                      || e.contactInfo.lastName.Contains(input.keyword)
                                      ));
             
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
        public List<MemberVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.members
                              .Include(i => i.contactInfo)
                            .Include(a => a.memberRoleLookupses)
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(int memberId)
        {
            using (var db = new MainDb())
            {
                var res = db.members
                     .Where(e => e.memberId == memberId)
                     .Delete();
                return true;
            } 
        } 

        public MemberVo update(MemberVo input, int? memberId= null)
        {
        
            using (var db = new MainDb())
            {

                if (memberId == null)
                    memberId = input.memberId; 

                var res = db.members.Include(i => i.contactInfo).FirstOrDefault(e => e.memberId == memberId);

                if (res == null) return null;

                input.created = res.created;

                if (!string.IsNullOrEmpty(input.passwordReset))
                    input.password = CurrentMember.HashWord(input.passwordReset);
                else 
                     input.password = res.password;

               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public MemberVo insert(MemberVo input)
        {
            using (var db = new MainDb())
            {
                
                db.members.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.members.Count();
            }
        }
		 
        
    }
}

