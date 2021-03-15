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
    public class ContactInfoManagerBase
    {

        public ContactInfoManagerBase() 
        {
		 
        }

        /// <summary>
        /// Find ContactInfo matching the contactInfoId (primary key)
        /// </summary>
        public ContactInfoVo get(int  contactInfoId )
        {
            using (var db = new MainDb())
            {
                var res = db.contactInfos
                            .FirstOrDefault(p => p.contactInfoId == contactInfoId);
                  
                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public ContactInfoVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.contactInfos
                            .FirstOrDefault();
               
                return res;
            }
        } 

		public SearchFilterVm search(SearchFilterVm input)
        {
            int keywordInt = int.Parse(input.keyword ?? "0");
            using (var db = new MainDb())
            {
                var query = db.contactInfos
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                      && (e.firstName.Contains(input.keyword) || e.lastName.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword)
                                      || e.contactInfoId == keywordInt
                                      || e.address.Contains(input.keyword)
                                      || e.city.Contains(input.keyword)
                                      || e.state.Contains(input.keyword)
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
        public List<ContactInfoVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.contactInfos
                             .Where(e => isActive==null || e.isActive == isActive )
                             .ToList();

                return list;
            }
        }


        public bool delete(int contactInfoId)
        {
            using (var db = new MainDb())
            {
                var res = db.contactInfos
                     .Where(e => e.contactInfoId == contactInfoId)
                     .Delete();
                return true;
            } 
        } 

        public ContactInfoVo update(ContactInfoVo input, int? contactInfoId= null)
        {
        
            using (var db = new MainDb())
            {

                if (contactInfoId == null)
                    contactInfoId = input.contactInfoId; 

                var res = db.contactInfos.FirstOrDefault(e => e.contactInfoId == contactInfoId);

                if (res == null) return null;

                if (input.state != null)
                    input.state = input.state.ToUpper();

                input.created = res.created;
               // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);

                 
                db.SaveChanges();
                return res;

            }
        }

        public ContactInfoVo insert(ContactInfoVo input)
        {
            using (var db = new MainDb())
            {


                if (input.state != null)
                    input.state = input.state.ToUpper();

                db.contactInfos.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.contactInfos.Count();
            }
        }
		 
        
    }
}

