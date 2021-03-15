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


namespace SO.Urba.Managers.Base
{
    public class OrganizationManagerBase
    {

        public OrganizationManagerBase()
        {

        }

        /// <summary>
        /// Find ContactCategoryMembership matching the contactCategoryMembershipId (primary key)
        /// </summary>
        public OrganizationVo get(int organizationId)
        {
            using (var db = new MainDb())
            {
                var res = db.organizations
                            .Include(i => i.contactInfo)
                            .FirstOrDefault(p => p.organizationId == organizationId);

                return res;
            }
        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public OrganizationVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.organizations
                    .Include(i => i.contactInfo)
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
                var query = db.organizations
                    .Include(i => i.contactInfo)
                             .Where(e => ((input.isActive == null || e.isActive == input.isActive)
                                        && (e.name.Contains(input.keyword) || string.IsNullOrEmpty(input.keyword)))
                                      || e.contactInfo.firstName.Contains(input.keyword)
                                      || e.contactInfo.lastName.Contains(input.keyword)
                                      || e.contactInfo.address.Contains(input.keyword)
                                      || e.organizationId == keywordInt
                                      || e.contactInfoId == keywordInt
                                      || e.feeAmount == keywordInt
                                      || e.contactInfo.address.Contains(input.keyword)
                                      || e.contactInfo.city.Contains(input.keyword)
                                    );
                query = query
                   .OrderByDescending(b => b.created);
                if (input.paging != null)
                {
                    input.paging.totalCount = query.Count();
                    query = query
                            .Skip(input.paging.skip)
                            .Take(input.paging.rowCount);

                }

                input.result = query.ToList<object>();

                return input;
            }
        }



        public SearchFilterVm searchForOrgOverview(SearchFilterVm input)
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
                if (input.paging != null)
                {
                    input.paging.totalCount = query.Count();
                    query = query
                            .Skip(input.paging.skip)
                            .Take(input.paging.rowCount);

                }

                input.result = query.ToList<object>();

                return input;
            }
        }

        public SearchFilterVm searchForOrgOverviewMembers(int orgID, SearchFilterVm input)
        {

            int keywordInt = 0;

            int.TryParse(input.keyword, out keywordInt);
            if (keywordInt < 0)
                keywordInt = 0;
            List<int> clientsOrgLookupIDs;
            ContactInfoVo contact;
            List<ContactInfoVo> query = new List<ContactInfoVo>();
            using (var db = new MainDb())
            {
                clientsOrgLookupIDs = db.clientOrganizationLookups
                    .Where(e => (e.organizationId == orgID))
                    .Select(e => e.clientId)
                    .ToList();
                if (clientsOrgLookupIDs.Count() > 0)
                {
                    foreach (int i in clientsOrgLookupIDs)
                    {
                        contact = db.clients
                                    .Where(c => (c.clientId == i))
                                    .Select(c => c.contactInfo).FirstOrDefault();
                        query.Add(contact);
                    }
                }
                if (query.Count() > 0) {
                    foreach (ContactInfoVo con in query) {
                        con.feeAmount = db.clients
                                .Where(c => (c.contactInfoId == con.contactInfoId))
                                .Select(c => c.feeAmount).FirstOrDefault();
                    }
                }
            }
            var query1 = query.AsQueryable();
            if (input.paging != null)
            {
                input.paging.totalCount = query1.Count();
                query1 = query1
                        .Skip(input.paging.skip)
                        .Take(input.paging.rowCount);

            }

            input.result = query1.ToList<object>();

            return input;
        }

        public List<OrganizationVo> getAll(bool? isActive = true)
        {
            using (var db = new MainDb())
            {
                var list = db.organizations
                    .Include(i => i.contactInfo)
                             .Where(e => isActive == null || e.isActive == isActive)
                             .ToList();

                return list;
            }
        }

        public List<OrganizationVo> getAllForOrgOverView(bool? isActive = true)
        {
            List<OrganizationVo> orgs = new List<OrganizationVo>();
            using (var db = new MainDb())
            {
                orgs = db.organizations
                             .Include(i => i.contactInfo)
                             .Where(e => isActive == null || e.isActive == isActive)
                             .ToList();

                return orgs;
            }
        }
        public bool delete(int organizationId)
        {
            using (var db = new MainDb())
            {
                var res = db.organizations
                     .Where(e => e.organizationId == organizationId)
                     .Delete();
                return true;
            }
        }

        public OrganizationVo update(OrganizationVo input, int? organizationId = null)
        {

            using (var db = new MainDb())
            {

                if (organizationId == null)
                    organizationId = input.organizationId;

                var res = db.organizations.FirstOrDefault(e => e.organizationId == organizationId);

                if (res == null) return null;

                input.created = res.created;
                db.Entry(res).CurrentValues.SetValues(input);


                db.SaveChanges();
                return res;

            }
        }

        public OrganizationVo insert(OrganizationVo input)
        {
            using (var db = new MainDb())
            {

                db.organizations.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.organizations.Count();
            }
        }
    }
}

