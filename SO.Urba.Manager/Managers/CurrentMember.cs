using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SO.Urba.Managers;
using SO.Urba.Models.ValueObjects;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;

namespace SO.Urba.Managers
{
    public class CurrentMember
    {
        private static string sessionKey = "_CurrentMember";
        private static MemberManager memberManager = new MemberManager();
        private static MemberRoleTypeManager memberRoleTypeManager = new MemberRoleTypeManager();

        public CurrentMember()
        {
            
        }

        public static List<string> getRoleNames()
        {
            var list  = member.memberRoleLookupses.Select(c=>c.memberRoleType.name).ToList();
            return list;
        }
        public static bool hasRoleOf(string role)
        {
            
            if(role != null && member != null)
            {

                var exists = member.memberRoleLookupses
                                .Any(c => c.memberRoleType.name.Equals(role, StringComparison.CurrentCultureIgnoreCase));
                 
                return exists;
            }
            return false;
        }
        public static bool hasRoleOf(int roleId)
        {

            if (roleId != null && member != null)
            {

                var exists = member.memberRoleLookupses
                                .Any(c => c.memberRoleType.memberRoleTypeId.Equals(roleId));

                return exists;
            }
            return false;
        }
        public static void reload()
        {
            member = memberManager.get(member.memberId);
        }
        public static  bool validateUser(string username, string password)
        {
            string hashedPassword = HashWord(password);
            //string hashedPassword = GetHash(password, "SHA1");

            MemberVo user = memberManager.getByUsernameAndPassword(username, hashedPassword);
            if (user == null)
                return false;
            
            return true;
        }

        public static IIdentity currentUser
        {
            get
            {
                try
                {
                   
                    return HttpContext.Current.User.Identity;
                }
                catch 
                { 
                    return null; 
                }
            }
        }

        public static bool isAuthenticated
        {
            get
            {
                try { 
                    return currentUser.IsAuthenticated; 
                }
                catch 
                { 
                    return false; 
                }
            }
        }


        #region current member

        public static MemberVo member
        {
            get
            {
                try
                {
                    if (!isAuthenticated)
                    {
                      
                        HttpContext.Current.Session[sessionKey] = null;
                        return null;
                    }

                    var mem = (MemberVo)HttpContext.Current.Session[sessionKey];

                    if (mem == null)
                    {
                        mem = memberManager.getByUsername(currentUser.Name);
                        if (mem != null)
                            HttpContext.Current.Session[sessionKey] = mem;
                    }

                    return mem;
                }
                catch (Exception /*ex*/)
                {
                    return null;
                }

            }
            set
            {
                try
                {
                    HttpContext.Current.Session[sessionKey] = value;
                }
                catch (Exception /*ex*/)
                { 
                    return; 
                }
            }
        }
        public static string GetHash(string stringToHash, string hashAlgorithm)
        {
            var algorithm = System.Security.Cryptography.HashAlgorithm.Create(hashAlgorithm);
            byte[] hash = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(stringToHash));

            // ToString("x2")  converts byte in hexadecimal value
            string encryptedVal = string.Concat(hash.Select(b => b.ToString("x2"))).ToUpperInvariant();
            return encryptedVal;
        }
        public static string HashWord(string WordToHash)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(WordToHash, "SHA1");
        }

        #endregion

    }
}
