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
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;

namespace SO.Urba.Managers
{
    public class OrganizationManager : OrganizationManagerBase
    {

        public OrganizationManager()
        {
        }

        //
        public SearchFilterVm organizationListExport(SearchFilterVm input)
        {

            int keywordInt = 0;

            int.TryParse(input.keyword, out keywordInt);
            if (keywordInt < 0)
                keywordInt = 0;

            using (var db = new MainDb())
            {
                var query = db.organizations
                    .Include(i => i.contactInfo)
                                                          .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                        && (e.name.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword)
                                      || e.contactInfo.firstName.Contains(input.keyword)
                                      || e.contactInfo.lastName.Contains(input.keyword)
                                      || e.contactInfo.address.Contains(input.keyword)
                                      || e.organizationId == keywordInt
                                      || e.contactInfoId == keywordInt
                                      || e.feeAmount == keywordInt
                                      || e.contactInfo.address.Contains(input.keyword)
                                      || e.contactInfo.city.Contains(input.keyword)
                                    ));
                if (query.Count() > 0)
                {
                    List<int> members = new List<int>();
                    foreach (OrganizationVo org in query)
                    {
                        members = OrganizationManager.getCountofMembersInOrg(org);
                        org.totalAmountofMembers = members.Count;
                    }
                }

                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object>();

                    items.Add(
                                new
                                {
                                    h1 = "Organization Name",
                                    h2 = "Fee Amount",
                                    h3 = "Has Paid Fee",
                                    h4 = "Contact Name",
                                    h5 = "Contact Work Phone",
                                    h6 = "Contact Email",
                                    h7 = "Total Members in Organization"
                                });

                    var item2 = query.AsEnumerable().Select(i =>
                                new
                                {
                                    Name = i.name,
                                    Fee_Amount = (i.feeAmount == null ? 0 : i.feeAmount),
                                    HasPaidFee = (i.hasPaidFee == null ? "N/A" : (i.hasPaidFee == true ? "Yes" : "No")),
                                    Contact_Name = i.contactInfo.firstName + " " + i.contactInfo.lastName,
                                    Work_Phone = i.contactInfo.workPhone,
                                    Email = (i.contactInfo == null ? "" : i.contactInfo.email ?? ""),
                                    Total_Members = i.totalAmountofMembers
                                }).ToList<object>();

                    foreach (var i in item2)
                    {
                        items.Add(i);
                    }

                    input.result = items;
                }

                return input;
            }
        }
        public SearchFilterVm organizationListExportForCertainOrg(int id, SearchFilterVm input)
        {
            using (var db = new MainDb())
            {
                var query = db.organizations
                    .Include(i => i.contactInfo)
                             .Where(e => (e.organizationId == id));
                if (query.Count() > 0)
                {
                    List<int> members = new List<int>();
                    foreach (OrganizationVo org in query)
                    {
                        members = OrganizationManager.getCountofMembersInOrg(org);
                        org.totalAmountofMembers = members.Count;
                    }
                }

                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object>();

                    items.Add(
                                new
                                {
                                    h1 = "Organization Name",
                                    h2 = "Fee Amount",
                                    h3 = "Has Paid Fee",
                                    h4 = "Contact Name",
                                    h5 = "Contact Work Phone",
                                    h6 = "Contact Email",
                                    h7 = "Total Members in Organization"
                                });

                    var item2 = query.AsEnumerable().Select(i =>
                                new
                                {
                                    Name = i.name,
                                    Fee_Amount = (i.feeAmount == null ? 0 : i.feeAmount),
                                    HasPaidFee = (i.hasPaidFee == null ? "N/A" : (i.hasPaidFee == true ? "Yes" : "No")),
                                    Contact_Name = i.contactInfo.firstName + " " + i.contactInfo.lastName,
                                    Work_Phone = i.contactInfo.workPhone,
                                    Email = (i.contactInfo == null ? "" : i.contactInfo.email ?? ""),
                                    Total_Members = i.totalAmountofMembers
                                }).ToList<object>();

                    foreach (var i in item2)
                    {
                        items.Add(i);
                    }

                    input.result = items;
                }

                return input;
            }
        }
        public decimal orgExists(string orgName)
        {
            using (var db = new MainDb())
            {
                var res = db.organizations.FirstOrDefault(e => e.name == orgName).feeAmount;

                return (decimal)res;
            }
        }
        public IEnumerable<string> getAllOrganizationNames()
        {
            using (var db = new MainDb())
            {
                var res = db.organizations
                    .Select(x => x.name).ToList();
                return res;
            }
        }
        public IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
    {
        typeof(int),typeof(double),typeof(decimal),
        //typeof(long), typeof(short),   typeof(sbyte),
        //typeof(byte), typeof(ulong),   typeof(ushort),
        //typeof(uint), typeof(float)
    };

        public static bool IsNumeric(Type myType)
        {
            return NumericTypes.Contains(myType);
        }
        private static List<OrganizationVo> getAllOrgsForResetFeeAmount(bool? isActive = true)
        {
            List<OrganizationVo> orgs = new List<OrganizationVo>();
            using (var db = new MainDb())
            {
                orgs = db.organizations
                             .Where(e => isActive == null || e.isActive == isActive)
                             .ToList();

                return orgs;
            }
        }
        private static void updateOrganizationFeeAmountForReferralRecordController(OrganizationVo currentOrg, decimal? memberfeeAmount, decimal? oldFinalPayment)
        {
            using (var db = new MainDb())
            {
                if (oldFinalPayment != null) { currentOrg.feeAmount -= oldFinalPayment; }
                currentOrg.feeAmount += memberfeeAmount;
                db.organizations.Attach(currentOrg);
                db.Entry(currentOrg).Property("feeAmount").IsModified = true;
                db.SaveChanges();
            }
        }
        public static void updateOrganizationFeeAmountForClientController(OrganizationVo currentOrg, decimal? memberfeeAmount)
        {
            using (var db = new MainDb())
            {
                currentOrg.feeAmount += memberfeeAmount;
                db.organizations.Attach(currentOrg);
                db.Entry(currentOrg).Property("feeAmount").IsModified = true;
                db.SaveChanges();
            }
        }
        public static void resetAllOrganizationsFeeAmountToZero() {
        List<OrganizationVo> orgs = OrganizationManager.getAllOrgsForResetFeeAmount();
            foreach (OrganizationVo org in orgs) 
            {
                OrganizationManager.resetOrganizationFeeAmountToZero(org);
            }
        }
        private static void resetOrganizationFeeAmountToZero(OrganizationVo currentOrg)
        {
            using (var db = new MainDb())
            {
                currentOrg.feeAmount = 0;
                db.organizations.Attach(currentOrg);
                db.Entry(currentOrg).Property("feeAmount").IsModified = true;
                db.SaveChanges();
            }
        }
        public static List<int> getCountofMembersInOrg(OrganizationVo org)
        {
            using (var db = new MainDb())
            {
                var res = db.clientOrganizationLookups
                      .Where(e => (e.organizationId == org.organizationId))
                      .Select(e => e.clientId).ToList();
                return res;
            }
        }
        public static List<int> getCountofMembersInOrgByID(int orgID)
        {
            using (var db = new MainDb())
            {
                var res = db.clientOrganizationLookups
                      .Where(e => (e.organizationId == orgID))
                      .Select(e => e.clientId).ToList();
                return res;
            }
        }
        public static int getOrgIDForClient(int clientID)
        {
            int orgID;
            using (var db = new MainDb())
            {
                orgID = db.clientOrganizationLookups
                      .Where(e => (e.clientId == clientID))
                      .Select(e => e.organizationId).FirstOrDefault();
            }
            return orgID;
        }
        public static string getOrgNameByID(int orgID)
        {
            string orgName = ""; 
            using (var db = new MainDb())
            {
                orgName = db.organizations
                      .Where(e => (e.organizationId == orgID))
                      .Select(e => e.name).FirstOrDefault();
            }
            return orgName;
        }
        public static void addAllFinalPaymentsForMembertoOrganizationFeeAmount(OrganizationVo currentOrg, decimal? memberFeeAmount, decimal? oldFinalPayment)
        {
            if (currentOrg.modified.Year < DateTime.Now.Year)
            {
                OrganizationManager.resetOrganizationFeeAmountToZero(currentOrg);
            }

                OrganizationManager.updateOrganizationFeeAmountForReferralRecordController(currentOrg, memberFeeAmount, oldFinalPayment);
        }
        }
}

