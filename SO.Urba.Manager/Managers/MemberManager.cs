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

namespace  SO.Urba.Managers 
{ 
    public class MemberManager : MemberManagerBase
    {
	 
        public MemberManager()
        {
		 
        }
        public MemberVo getByUsernameAndPassword(string username, string hashedPassword)
        {
            using (var db = new MainDb())
            {
                MemberVo mem = null;

                try
                {
                    mem = db.members
                            .Include(i => i.contactInfo)
                           .Include(a => a.memberRoleLookupses.Select(c=>c.memberRoleType))
                            .FirstOrDefault(p => p.username == username && p.password == hashedPassword);
                }
                catch (Exception ex)
                {
                    mem = null;
                }

                return mem;
            }
        }

        public MemberVo getByUsername(string usernameOrEmail)
        {
            using (var db = new MainDb())
            {
                var mem = db.members
                            .Include(i => i.contactInfo)
                            .Include(a => a.memberRoleLookupses.Select(c => c.memberRoleType))
                            .FirstOrDefault(p => p.username == usernameOrEmail);

                return mem;
            }
        }


    }
}

