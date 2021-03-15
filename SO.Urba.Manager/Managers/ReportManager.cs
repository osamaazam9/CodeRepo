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
using SO.Urba.Models.ViewModels;
using System.Data.Entity.SqlServer;
using System.Web.Mvc;
using SO.Urba.Manager.Models.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace SO.Urba.Managers
{
    public class ReportManager : ReportManagerBase
    {

        public ReportManager()
        {

        }

        // 1. works
        public ReportSearchFilterVm categoryReferralByDate(ReportSearchFilterVm input)
        {
            var fromDate = input.fromDate.AddDays(-1);
            var toDate = input.toDate.AddDays(1);
            using (var db = new MainDb())
            {
                var query = db.companyCategoryTypes
                        .Include(i => i.referralses)
                        .Include(k => k.referralses.Select(v => v.surveyAnswerses))
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                 && (e.referralses.Any(v => v.created > fromDate) && e.referralses.Any(v => v.created < toDate))
                                    );
                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object> { new { PERIOD = fromDate.ToShortDateString() + " thru " + toDate.ToShortDateString() } };

                    items.Add(
                                new
                                {
                                    h1 = "Provider Category",
                                    h2 = "Referrals",
                                    h3 = "Quotes",
                                    h4 = "Accepts",
                                    h5 = "Quotes Total",
                                    h6 = "Final Quotes Total"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    Provider_Category = i.name,
                                    Referrals = i.referralses.Distinct().Count(),
                                    Quotes = i.referralses.Where(d => d.quote != null).Count(),
                                    Accepts = i.referralses.Where(d => d.accepted == true).Count(),
                                    Quotes_Total = (i.referralses.Sum(d => d.quote) != null ? i.referralses.Sum(d => d.quote) : 0),
                                    Final_Quotes_Total = (i.referralses.Sum(d => d.finalQuote) != null ? i.referralses.Sum(d => d.finalQuote) : 0)
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

        // 2. works
        public ReportSearchFilterVm categorySatisfactionByDate(ReportSearchFilterVm input)
        {
            var fromDate = input.fromDate.AddDays(-1);
            var toDate = input.toDate.AddDays(1);
            using (var db = new MainDb())
            {
                var query = db.companyCategoryTypes
                        .Include(i => i.referralses)
                        .Include(k => k.referralses.Select(v => v.surveyAnswerses))
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                 && (e.referralses.Any(v => v.created > fromDate) && e.referralses.Any(v => v.created < toDate))
                                    );
                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object> { new { PERIOD = fromDate.ToShortDateString() + " thru " + toDate.ToShortDateString() } };

                    items.Add(
                                new
                                {
                                    h1 = "Provider Category",
                                    h2 = "Referrals",
                                    h3 = "Very Good",
                                    h4 = "Good",
                                    h5 = "Average",
                                    h6 = "Bad",
                                    h7 = "Very Bad"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    ProviderCategory = i.name,
                                    Referrals = i.referralses.Distinct().Count(),
                                    VeryGood = i.referralses.Sum(r => r.surveyAnswerses.Count(e => e.answer == 5)),
                                    Good = i.referralses.Sum(r => r.surveyAnswerses.Count(e => e.answer == 4)),
                                    Average = i.referralses.Sum(r => r.surveyAnswerses.Count(e => e.answer == 3)),
                                    Bad = i.referralses.Sum(r => r.surveyAnswerses.Count(e => e.answer == 2)),
                                    VeryBad = i.referralses.Sum(r => r.surveyAnswerses.Count(e => e.answer == 1))
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

        // 3. just double check Score calculation/float
        public ReportSearchFilterVm prodiverReferralByDate(ReportSearchFilterVm input)
        {
            var fromDate = input.fromDate.AddDays(-1);
            var toDate = input.toDate.AddDays(1);
            using (var db = new MainDb())
            {
                var query = db.referrals
                    .Include(i => i.company)
                    .Include(k => k.client)
                    .Include(k => k.client.contactInfo)
                    .Include(v => v.surveyAnswerses)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                    && (e.companyId == input.companyId)
                                    && (e.created > fromDate)
                                    && (e.created < toDate)
                                    );

                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var first = query.FirstOrDefault();
                    var signedDate = first.company.agreementSigned.ToString().Split(" ")[0];

                    var items = new List<object> {
                                new
                                {
                                    h1 = first.company.companyName,
                                    h2 = "Work:",
                                    h3 = first.client.contactInfo.workPhone
                                }};
                    items.Add((object)
                                new
                                {
                                    h1 = first.client.contactInfo.fullname,
                                    h2 = "Cell:",
                                    h3 = first.client.contactInfo.cell,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = first.client.contactInfo.address + ", " + first.client.contactInfo.city
                                      + " " + first.client.contactInfo.state + " " + first.client.contactInfo.zip,
                                    h2 = "Fax:",
                                    h3 = first.client.contactInfo.fax,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "ID: " + first.companyId,
                                    h2 = "Email:",
                                    h3 = first.client.contactInfo.email,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "Lisence Number: " + first.company.license,
                                    h2 = "Agreement Signed:",
                                    h3 = signedDate
                                });
                    items.Add(new
                    {
                        h1 = "ID",
                        h2 = "Homeowner",
                        h3 = "Job",
                        h4 = "Referral Date",
                        h5 = "Status",
                        h6 = "Score"
                    });
                    var item3 = query.Select(i =>
                                new
                                {
                                    ID = i.referralId,
                                    Homeowner = i.client.contactInfo.firstName + " " + i.client.contactInfo.lastName,
                                    Job = i.description,
                                    Ref_Date = SqlFunctions.DateName("day", i.referralDate).Trim() + "/" +
                                               SqlFunctions.StringConvert((double)i.referralDate.Value.Month).TrimStart() + "/" +
                                               SqlFunctions.DateName("year", i.referralDate),
                                    Status = (i.accepted == true ? "accepted" : "pending"),
                                    Score = (i.surveyAnswerses.Any(t => t.answer != null)
                                        ? (decimal?)i.surveyAnswerses.Sum(v => v.answer) * 20 / i.surveyAnswerses.Count() : null)
                                }).ToList<object>();

                    foreach (var i in item3)
                    {
                        items.Add(i);
                    }

                    input.result = items;
                }
                return input;
            }
        }

        // 4. add Dues calculation if applicable, and double check Score calculation/float
        public ReportSearchFilterVm clientReferralByDate(ReportSearchFilterVm input)
        {
            var fromDate = input.fromDate.AddDays(-1);
            var toDate = input.toDate.AddDays(1);
            using (var db = new MainDb())
            {
                var query = db.referrals
                        .Include(i => i.client)
                        .Include(k => k.company)
                        .Include(n => n.surveyAnswerses)
                             .OrderByDescending(b => b.created)
                             .Where(e => (input.isActive == null || e.isActive == input.isActive)
                                 && (e.clientId == input.clientId)
                                 && (e.created > fromDate && e.created < toDate)
                                    );

                input.result = query.ToList<object>();

                var query2 = db.clients
                        .Include(i => i.contactInfo)
                        .Include(i => i.clientOrganizationLookupses.Select(c => c.organization))
                             .Where(e => (e.clientId == input.clientId)
                                    );

                if (input.result.Count() != 0)
                {
                    var cl = query2.FirstOrDefault();
                    var memStartDate = query2.FirstOrDefault().startingDate.ToString().Split(" ")[0];
                    var memEndDate = query2.FirstOrDefault().endDate.ToString().Split(" ")[0];

                    var items = new List<object> {
                                new
                                {
                                    h1 = cl.contactInfo.firstName + " " + cl.contactInfo.lastName,
                                    h2 = "Home:",
                                    h3 = cl.contactInfo.homePhone
                                }};
                    items.Add((object)
                                new
                                {
                                    h1 = cl.contactInfo.address + ", " + cl.contactInfo.city
                                      + " " + cl.contactInfo.state + " " + cl.contactInfo.zip,
                                    h2 = "Cell:",
                                    h3 = cl.contactInfo.cell,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "",
                                    h2 = "Email:",
                                    h3 = cl.contactInfo.email,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "Organization:",
                                    h2 = cl.clientOrganizationLookupses.FirstOrDefault().organization.name,
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "Membership:",
                                    h2 = (cl.startingDate == null && cl.endDate == null
                                           ? "" : memStartDate + " thru " + memEndDate),
                                });
                    items.Add((object)
                                new
                                {
                                    h1 = "Dues:",
                                    h2 = "", // ???
                                });

                    items.Add(
                                new
                                {
                                    h1 = "ID",
                                    h2 = "Contractor",
                                    h3 = "Job",
                                    h4 = "Referral Date",
                                    h5 = "Status",
                                    h6 = "Score"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    ID = i.referralId,
                                    Contractor = i.company.companyName,
                                    Job = i.description,
                                    Ref_Date = SqlFunctions.DateName("day", i.referralDate).Trim() + "/" +
                                               SqlFunctions.StringConvert((double)i.referralDate.Value.Month).TrimStart() + "/" +
                                               SqlFunctions.DateName("year", i.referralDate),
                                    Status = (i.accepted == true ? "accepted" : "pending"),
                                    Score = (i.surveyAnswerses.Any(t => t.answer != null) ? (decimal?)i.surveyAnswerses.Sum(v => v.answer) * 20 / i.surveyAnswerses.Count() : null)

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


        //------------New Functionality: Category Referral-------------------------------------------------------------------------
        public IEnumerable<CompanyCategoryTypeVo> getAllCategoryNames()
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryTypes
                    .OrderBy(i => i.name).ToList();
                return res;
            }
        }
        //Create select list of all category names
        public IEnumerable<SelectListItem> getSelectListForCategoryItems(IEnumerable<CompanyCategoryTypeVo> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.name,
                    Text = element.name
                });
            }

            return selectList;
        }
        public static int getCategoryId(string categoryName)
        {
            using (var db = new MainDb())
            {
                var res = db.companyCategoryTypes.FirstOrDefault(e => e.name == categoryName).companyCategoryTypeId;

                return res;
            }
        }
        public SpecificCategoryReferralsByDateVM SpecificCategoryReferralsByDateFileSearch(SpecificCategoryReferralsByDateVM input)
        {
            var fromDate = input.fDate.AddDays(-1);
            var toDate = input.tDate.AddDays(1);
            int categoryID = ReportManager.getCategoryId(input.categoryName);

            using (var db = new MainDb())
            {
                var query = db.referrals
                          .Include(i => i.companyCategoryType)
                          .Include(p => p.company)
                          .Include(q => q.client)
                          .Include(w => w.client.contactInfo)
                          .Where(e => (e.companyCategoryTypeId == categoryID)
                          && (e.referralDate > fromDate)
                          && (e.referralDate < toDate)).ToList();

                input.result = query.ToList<object>();

                if (input.result.Count() != 0)
                {
                    var items = new List<object> { new { PERIOD = fromDate.ToShortDateString() + " thru " + toDate.ToShortDateString() } };
                    items.Add(new { Client = "Count of Referrals: " + (input.result.Count()) });
                    items.Add(
                                new
                                {
                                    h1 = "Category Name",
                                    h2 = "Company Name",
                                    h3 = "Client Name",
                                    h4 = "Referral Date",
                                    h5 = "Quote",
                                    h6 = "Commission",
                                    h7 = "Service description"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    Category = (i.companyCategoryType.name != null ? i.companyCategoryType.name : "N/A"),
                                    CompanyName = (i.company.name != null ? i.company.name : "N/A"),
                                    ClientName = ((i.client.contactInfo != null) && (i.client.contactInfo.name != null) ? i.client.contactInfo.name : "N/A"),
                                    ReferralDate = (i.created != null ? i.created.ToShortDateString() : "N/A"),
                                    Quote = (i.quote != null ? i.quote : 0),
                                    Commission = (i.commissionAmount != null ? i.commissionAmount : 0),
                                    Servicedescription = (i.description != null ? i.description : "N/A")
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
        //Category Referral Feature Finsih-----------------------------------------------------

        //New Functionality: All Referrals for a given time period-----------------------------
        public AllReferralsByDateVM AllReferralsByDateFileSearch(AllReferralsByDateVM input)
        {
            var fromDate2 = input.fDate2.AddDays(-1);
            var toDate2 = input.tDate2.AddDays(1);

            using (var db = new MainDb())
            {
                var query = db.referrals
                          .Include(i => i.companyCategoryType)
                          .Include(p => p.company)
                          .Include(q => q.client)
                          .Include(w => w.client.contactInfo)
                          .Where(e => (e.referralDate > fromDate2)
                          && (e.referralDate < toDate2)).ToList();

                input.allReferrals = query.ToList<object>();

                if (input.allReferrals.Count() != 0)
                {
                    var items = new List<object> { new { PERIOD = fromDate2.ToShortDateString() + " thru " + toDate2.ToShortDateString() } };
                    items.Add(new { Client = "Count of Referrals: " + (input.allReferrals.Count()) });

                    items.Add(
                                new
                                {
                                    h1 = "Category Name",
                                    h2 = "Company Name",
                                    h3 = "Client Name",
                                    h4 = "Referral Date",
                                    h5 = "Quote",
                                    h6 = "Commission",
                                    h7 = "Service description"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    Category = (i.companyCategoryType.name != null ? i.companyCategoryType.name : "N/A"),
                                    CompanyName = (i.company.name != null ? i.company.name : "N/A"),
                                    ClientName = ((i.client.contactInfo != null) && (i.client.contactInfo.name != null) ? i.client.contactInfo.name : "N/A"),
                                    ReferralDate = (i.created != null ? i.created.ToShortDateString() : "N/A"),
                                    Quote = (i.quote != null ? i.quote : 0),
                                    Commission = (i.commissionAmount != null ? i.commissionAmount : 0),
                                    Servicedescription = (i.description != null ? i.description : "N/A")
                                }).ToList<object>();

                    foreach (var i in item2)
                    {
                        items.Add(i);
                    }

                    input.allReferrals = items;
                }

                return input;
            }
        }
        //All Referrals for a given time period Finsih---------------------------------------------

        //All Referrals for a client for a given time period ----------------------------------------------

        private static string getOrgNameForClient(int clientID)
        {
            using (var db = new MainDb())
            {
                int orgID = db.clientOrganizationLookups
                    .Where(e => (e.clientId == clientID))
                    .Select(e => e.organizationId).FirstOrDefault();

                string orgName = db.organizations
                    .Where(x => (x.organizationId == orgID))
                    .Select(x => x.name).FirstOrDefault();
                return orgName;
            }
        }
        private static ContactInfoVo getClientContactInfo(int clientID) {
            using (var db = new MainDb())
            {
                int contactInfoID = db.clients
                    .Where(z => (z.clientId == clientID))
                    .Select(z => z.contactInfoId).FirstOrDefault();
                ContactInfoVo clientInfo = db.contactInfos
                     .Where(e => (e.contactInfoId == contactInfoID))
                     .FirstOrDefault();
                return clientInfo;
            }
        }

        public IEnumerable<ClientVo> getAllClients()
        {
            using (var db = new MainDb())
            {
                var list = db.clients
                            .Include(i => i.contactInfo)
                            .Include(o => o.clientOrganizationLookupses)
                            .OrderBy(i => i.contactInfo.firstName).ToList();

                return list;
            }
        }
        //Create select list of all category names
        public IEnumerable<SelectListItem> getSelectListForClientsItems(IEnumerable<ClientVo> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.clientId.ToString(),
                    Text = element.contactInfo.fullname,
                });
            }

            return selectList;
        }

        public AllReferralsForaClientVM AllReferralsForaClientFileSearch(AllReferralsForaClientVM input)
        {
            var fromDate3 = input.fDate3.AddDays(-1);
            var toDate3 = input.tDate3.AddDays(1);
            var organizationName = ReportManager.getOrgNameForClient(input.clientId1);
            var clientContactInfo = ReportManager.getClientContactInfo(input.clientId1);
            using (var db = new MainDb())
            {
                var query = db.referrals
                          .Include(i => i.companyCategoryType)
                          .Include(p => p.company)
                          .Include(q => q.client)
                          .Include(w => w.client.contactInfo)
                          .Where(e => (e.clientId == input.clientId1)
                          && (e.referralDate > fromDate3)
                          && (e.referralDate < toDate3)).ToList();

                input.allReferralsForaClient = query.ToList<object>();
                if (input.allReferralsForaClient.Count() != 0)
                {
                    var items = new List<object> { new { PERIOD = fromDate3.ToShortDateString() + " thru " + toDate3.ToShortDateString() } };
                    items.Add(new { Client = "Count of Referrals: " + (input.allReferralsForaClient.Count()) });
                    items.Add(new {Client = "Client's Organization Name: " + (organizationName != null ? organizationName.ToString() : "N/A") });
                    items.Add(new { Client = "Client's Name: " + ((clientContactInfo != null) && (clientContactInfo.name != null) ? clientContactInfo.name : "N/A") });
                    items.Add(new { Client = "Client's Email: " + ((clientContactInfo != null) && (clientContactInfo.email != null) ? clientContactInfo.email : "N/A") });
                    items.Add(new { Client = "Client's Phone Number: " + ((clientContactInfo != null) && (clientContactInfo.workPhone != null) ? clientContactInfo.workPhone : "N/A") });
                    items.Add(
                                new
                                {
                                    h1 = "Category Name",
                                    h2 = "Company Name",
                                    h3 = "Referral Date",
                                    h4 = "Quote",
                                    h5 = "Commission",
                                    h6 = "Service description"
                                });

                    var item2 = query.Select(i =>
                                new
                                {
                                    Category = (i.companyCategoryType.name != null ? i.companyCategoryType.name : "N/A"),
                                    CompanyName = (i.company.name != null ? i.company.name : "N/A"),
                                    ReferralDate = (i.created != null ? i.created.ToShortDateString() : "N/A"),
                                    Quote = (i.quote != null ? i.quote : 0),
                                    Commission = (i.commissionAmount != null ? i.commissionAmount : 0),
                                    Servicedescription = (i.description != null ? i.description : "N/A")
                                }).ToList<object>();

                    foreach (var i in item2)
                    {
                        items.Add(i);
                    }

                    input.allReferralsForaClient = items;
                }

                return input;
            }
        }

        //All Referrals for a client for a given time period Finish -----------------------------------------
    }
}

