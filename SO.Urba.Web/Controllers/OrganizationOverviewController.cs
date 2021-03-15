using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;
using SO.Urba.Models.ValueObjects;
using SO.Utility.Models.ViewModels;
using SO.Urba.DbContexts;
using SO.Urba.Managers;
using SO.Urba.Models.ViewModels;
using SO.Urba.Manager.Models.ViewModels;
using SO.Utility.Classes;
using SO.Utility;
using SO.Utility.Helpers;
using SO.Utility.Extensions;
using System.Net.Http;

namespace SO.Urba.Web.Controllers
{
    public class OrganizationOverviewController : Controller
    {
        private OrganizationManager organizationManager = new OrganizationManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();
        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Organizations Overview";
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = organizationManager.searchForOrgOverview(input);
                return View(input);
            }
            return View(input);
        }
    public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = organizationManager.organizationListExport(input);
                var file = ImportExportHelper.exportToCsv(input.result, false);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }
        public FileResult ExportForCertainOrg(int id, SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = organizationManager.organizationListExportForCertainOrg(id, input);
                var file = ImportExportHelper.exportToCsv(input.result, false);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }
        public ActionResult ResetOrgFeeAmount() {
            OrganizationManager.resetAllOrganizationsFeeAmountToZero();
            this.Response.Write("<script> window.location.href = '/OrganizationOverview'; </script>");
            return null;
        }
        public ActionResult List()
        {
            var results = organizationManager.getAllForOrgOverView(null);
            return PartialView(results);;
        }
    }
}