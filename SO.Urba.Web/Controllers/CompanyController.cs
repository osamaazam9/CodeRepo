using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;
using SO.Urba.Manager.Models.ViewModels;
using SO.Urba.Models.ValueObjects;
using SO.Urba.DbContexts;
using SO.Urba.Managers;
using SO.Urba.Models.ViewModels;
using SO.Utility.Classes;
using SO.Utility.Models.ViewModels;
using SO.Utility;
using SO.Utility.Helpers;
using SO.Utility.Extensions;

namespace  SO.Urba.Controllers
{
      [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private CompanyManager companyManager = new CompanyManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();
        private CompanyCategoryTypeManager companyCategoryTypeManager = new CompanyCategoryTypeManager();
        private CompanyCategoryLookupManager companyCategoryLookupManager = new CompanyCategoryLookupManager();
        private ReferralRecordManager referralRecordManager = new ReferralRecordManager();

        public ActionResult Index(CompanySearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Providers";

            if (input == null) input = new CompanySearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            { 
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = companyManager.search(input);
                return View(input);
            }
            return View(input);
        }
        
        public FileResult Export(CompanySearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                //input = companyManager.exportSearch(input);
                input = companyManager.contactsByProvider(input);
                var file = ImportExportHelper.exportToCsv(input.result, true);

                if (file != null)
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }

            return null;
        }

        public ActionResult DropDownList(int? id = null, string propertyName = null, Type modelType = null, string defaultValue = null)
        {
            //var item = Activator.CreateInstance(modelType);
            ViewBag.propertyName = propertyName;
            if (defaultValue == null)
                defaultValue = "Select One";
            ViewBag.defaultValue = defaultValue;

            ViewBag.selectedItem = id;
            if (modelType == typeof(CompanyCategoryTypeVo))
            {
                ViewBag.items = companyCategoryTypeManager.getAll(true);
                ViewBag.idName = "companyCategoryTypeId";
                return PartialView("_MultySelectDropDownList");
            }

            return PartialView("_DropDownList");
        }
	    #region CRUD

		public ActionResult List()
        {
            var results = companyManager.getAll(null);
            return PartialView(results);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, CompanyVo input)
        {
            bool foundTheMatch = false;
            CompanyVo item = companyManager.get(id);
            if (this.ModelState.IsValid)
            {
                if (item.companyCategories != null)
                {
                    foreach (CompanyCategoryLookupVo categoryLookupVo in item.companyCategoryLookupses)
                    {
                        foundTheMatch = false;
                        foreach (int categoryTypeId in input.companyCategories)
                        {
                            if (categoryLookupVo.companyCategoryTypeId == categoryTypeId)
                            {
                                input.companyCategories.Remove(categoryTypeId);
                                foundTheMatch = true;
                                break;
                            }
                        }
                        if (!foundTheMatch)
                            companyCategoryLookupManager.delete(categoryLookupVo.companyCategoryId);
                    }
                }
                if (input.companyCategories != null)
                {
                    
                    foreach (int categoryId in input.companyCategories)
                    {
                        var companyCategoryLookupVo = new CompanyCategoryLookupVo();
                        companyCategoryLookupVo.companyId = input.companyId;
                        companyCategoryLookupVo.companyCategoryTypeId = categoryId;
                        companyCategoryLookupVo.isActive = true;

                        companyCategoryLookupManager.insert(companyCategoryLookupVo);
                    }

                }
                contactInfoManager.update(input.contactInfo, input.contactInfo.contactInfoId);
                var res = companyManager.update(input, id);
                return RedirectToAction("Index");
            }
            return View(input); 

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var result = companyManager.get(id);
            if (result.companyCategories.Count > 0)
                result.companyCategories = new List<int>();
            if (result.companyCategoryLookupses != null)
            {
                foreach (var item in result.companyCategoryLookupses)
                {
                    result.companyCategories.Add((int)item.companyCategoryTypeId);
                }
            }
            return View(result); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(CompanyVo input)
        {

            if (this.ModelState.IsValid)
            {

                var item = companyManager.insert(input);
                 if (input.companyCategories != null)
                {
                    foreach (int categoryId in input.companyCategories)
                    {
                        var companyCategoryType = new CompanyCategoryLookupVo();
                        companyCategoryType.companyId = input.companyId;
                        companyCategoryType.companyCategoryTypeId = categoryId;
                        companyCategoryType.isActive = true;

                        companyCategoryLookupManager.insert(companyCategoryType);

                    }
                }
                return RedirectToAction("Index");
            }

            return View(input);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var vo = new CompanyVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = companyManager.get(id);
            return View(result);
        }

        public ActionResult Referrals(int id)
        {
            var result = companyManager.get(id);
            return View(result);
        }

        public ActionResult ReferralHistory(int? id = null, ProviderReferralHistoryFilter referralHistoryFilter = ProviderReferralHistoryFilter.All)
        {
            ViewBag.id = id;
            ViewBag.referralHistoryFilter = referralHistoryFilter;

            List<ReferralVo> data = referralRecordManager.getReferralsOfProvider(id, true, referralHistoryFilter);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ReferralHistory", data);
            }
            else
            {
                return View("_ReferralHistory", data);
            }
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            contactInfoManager.delete((int)companyManager.get(id).contactInfoId);
            companyManager.delete(id);
            return RedirectToAction("Index");
        }

		#endregion
    }	 
        
    
}

