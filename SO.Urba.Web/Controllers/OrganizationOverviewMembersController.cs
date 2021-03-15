using SO.Urba.Managers;
using SO.Utility.Classes;
using SO.Utility.Helpers;
using SO.Utility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SO.Urba.Web.Controllers
{
    public class OrganizationOverviewMembersController : Controller
    {
        private OrganizationManager organizationManager = new OrganizationManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();
        public ActionResult Index(int id, SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Organizations Overview Members";
            ViewBag.TotalMembers = OrganizationManager.getCountofMembersInOrgByID(id).Count();
            ViewBag.orgName = OrganizationManager.getOrgNameByID(id);
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = organizationManager.searchForOrgOverviewMembers(id, input);
                return View(input);
            }
            return View(input);
        }
    }
}