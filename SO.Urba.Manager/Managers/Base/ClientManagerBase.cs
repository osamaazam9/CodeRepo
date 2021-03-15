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
using SO.Urba.Models.ViewModels;


namespace  SO.Urba.Managers.Base
{
    public class ClientManagerBase
    {

        public ClientManagerBase()
        {

        }

        /// <summary>
        /// Find Client matching the contactMembershipId (primary key)
        /// </summary>
        public ClientVo get(int clientId)
        {
            using (var db = new MainDb())
            {
                var res = db.clients
                            .Include(i => i.contactInfo)
                            .Include(a => a.clientOrganizationLookupses.Select(c => c.organization))
                            .FirstOrDefault(p => p.clientId == clientId);

                return res;
            }

        }

        /// <summary>
        /// Get First Item
        /// </summary>
        public ClientVo getFirst()
        {
            using (var db = new MainDb())
            {
                var res = db.clients
                             .Include(i => i.contactInfo)
                            .Include(o => o.clientOrganizationLookupses)
                            .FirstOrDefault();

                return res;
            }
        }

        public SearchFilterVm search(SearchFilterVm input)
        {
            int keywordInt = 0;
            int.TryParse(input.keyword, out keywordInt);

            using (var db = new MainDb())
            {
                var query = db.clients
                    .Include(i => i.contactInfo)
                    //.Include(o => o.clientOrganizationLookupses)
                    .Include(a => a.clientOrganizationLookupses.Select(c => c.organization))
                    .Where(e => (input.isActive == null || e.isActive == input.isActive) && (e.contactInfo != null));

                switch (input.searchField)
                {
                    default:
                    case SearchField.All:
                        query = query
                            .Where(e =>
                                 string.IsNullOrEmpty(input.keyword)
                                 || e.contactInfo.firstName.Contains(input.keyword)
                                 || e.contactInfo.lastName.Contains(input.keyword)
                                 || e.contactInfo.address.Contains(input.keyword)
                                 || e.contactInfo.city.Contains(input.keyword)
                                 || e.contactInfo.state.Contains(input.keyword)
                                 || e.contactInfo.homePhone.Contains(input.keyword)
                                 || e.contactInfo.workPhone.Contains(input.keyword)
                                 || (keywordInt > 0 && e.clientId == keywordInt));
                        break;

                    case SearchField.FirstName:
                        query = query
                            .Where(e =>
                                 string.IsNullOrEmpty(input.keyword)
                                 || e.contactInfo.firstName.Contains(input.keyword));
                        break;

                    case SearchField.LastName:
                        query = query
                            .Where(e =>
                                 string.IsNullOrEmpty(input.keyword)
                                 || e.contactInfo.lastName.Contains(input.keyword));
                        break;

                    case SearchField.MemberID:
                        query = query
                            .Where(e => 
                                keywordInt > 0 
                                && e.clientId == keywordInt);
                        break;
                }

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

        public List<ClientVo> getAll(bool? isActive=true)
        {
            using (var db = new MainDb())
            {
                var list = db.clients
                    .Include(i => i.contactInfo)
                    .Include(o => o.clientOrganizationLookupses)
                    .Where(e => isActive==null || e.isActive == isActive )
                    .OrderBy(i => i.contactInfo.firstName).ToList();

                return list;
            }
        }


        public bool delete(int clientId)
        {
            using (var db = new MainDb())
            {
                var res = db.clients
                    .Include(i => i.contactInfo)
                    .Include(o => o.clientOrganizationLookupses)
                     .Where(e => e.clientId == clientId)
                     .Delete();
                return true;
            }
        }

        public ClientVo update(ClientVo input, int? clientId = null)
        {

            using (var db = new MainDb())
            {

                if (clientId == null)
                    clientId = input.clientId;

                var res = db.clients.FirstOrDefault(e => e.clientId == clientId);

                if (res == null) return null;

                input.created = res.created;
                // input.createdBy = res.createdBy;
                db.Entry(res).CurrentValues.SetValues(input);


                db.SaveChanges();
                return res;

            }
        }

        public ClientVo insert(ClientVo input)
        {
            using (var db = new MainDb())
            {
                db.clients.Add(input);
                db.SaveChanges();

                return input;
            }

        }

        public int count()
        {
            using (var db = new MainDb())
            {
                return db.clients.Count();
            }
        }


    }
}

