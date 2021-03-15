using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SO.Urba.Managers;

namespace SO.Urba.Web.Providers
{
    public class MemberRoleProvider : RoleProvider
    {
     
         /// 
        /// This method will override the getrolesforuser method in roleprovider and do the custom implementation 
        /// 
        /// 
        /// 
        public override string[] GetRolesForUser(string username)
        {
           
            var rolesArray = CurrentMember.getRoleNames().ToArray<string>();

            return rolesArray;
           
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
         
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }

}