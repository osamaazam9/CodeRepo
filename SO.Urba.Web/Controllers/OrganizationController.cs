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

namespace SO.Urba.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrganizationController : Controller
    {
        private OrganizationManager organizationManager = new OrganizationManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();
        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Organizations";
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = organizationManager.search(input);
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
        [HttpPost]
        public ActionResult RevenueSharing(RevenueSharingVM orgModel)
        {
            //(OrganizationManager.IsNumeric(orgModel.rate.GetType())) && (orgModel.rate > 0)
            try
            {
                if (ModelState.IsValid)
                {
                    decimal organizationFeeAmount = organizationManager.orgExists(orgModel.orgName);
                    decimal perct = orgModel.rate / 100;
                    ViewBag.ErrorMessage = "";
                    orgModel.totalGetBackAmount = organizationFeeAmount * perct;
                }
            }
            catch (Exception err)
            {
                ViewBag.ErrorMessage = "There is no fee amount for this organization";
            }
            return PartialView("_RevenueSharing", orgModel);

        }

        #region CRUD

        public ActionResult List()
        {
            var results = organizationManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Edit(int id, OrganizationVo input)
        {

            if (this.ModelState.IsValid)
            {
                contactInfoManager.update(input.contactInfo, input.contactInfoId);
                var res = organizationManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input);

        }
        public ActionResult Edit(int id)
        {
            var result = organizationManager.get(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Create(OrganizationVo input)
        {

            if (this.ModelState.IsValid)
            {

                var item = organizationManager.insert(input);
                return RedirectToAction("Index");
            }


            return View(input);

        }

        public ActionResult Create()
        {
            var vo = new OrganizationVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = organizationManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Delete(int id)
        {
            var cid = organizationManager.get(id).contactInfoId;
            organizationManager.delete(id);
            if (cid != null)
                contactInfoManager.delete((int)cid);
            return RedirectToAction("Index");
        }

        #endregion
    }


}

