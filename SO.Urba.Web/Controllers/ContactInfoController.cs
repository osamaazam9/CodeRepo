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
    public class ContactInfoController : Controller
    {
        private ContactInfoManager contactInfoManager = new ContactInfoManager();
        private CompanyManager companyManager = new CompanyManager();


        //public ActionResult Index(SO.Urba.Models.ViewModels.ContactInfoSearchFilterVm input = null, Paging paging = null)
        //{
        //    if (input == null) input = new ContactInfoSearchFilterVm();
        //    input.paging = paging;

        //    if (this.ModelState.IsValid)
        //    { 
        //        if (input.submitButton != null)
        //            input.paging.pageNumber = 1;
        //        input = contactInfoManager.search(input);
        //        return View(input);
        //    }
        //    return View(input);
        //}

        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = contactInfoManager.search(input);
                return View(input);
            }
            return View(input);
        }

        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = contactInfoManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }
		 
	    #region CRUD

		public ActionResult List()
        {
            var results = contactInfoManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Edit(int id, ContactInfoVo input)
        {

            if (this.ModelState.IsValid)
            {
                var res = contactInfoManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input); 

        }
        public ActionResult Edit(int id)
        {
            var result = contactInfoManager.get(id);
            return View(result); 
        }

        [HttpPost]
        public ActionResult Create(SO.Urba.Models.ViewModels.ContactInfoVm input)
        {
            if (this.ModelState.IsValid)
            {
                var res = contactInfoManager.insert(input);
                return RedirectToAction("Index");
            }

            return View(input);
        }

        public ActionResult Create()
        {
            //var vo = new ContactInfoVo();
            //return View(vo);
            var vm = new ContactInfoVm();
            vm.contactInfo.created = DateTime.Now;
            vm.contactInfo.modified = DateTime.Now;
            return View(vm);
        }

        //[HttpPost]
        //public ActionResult Create(ContactInfoVo input, int companyId)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        if (companyId == null)
        //            var res = contactInfoManager.insert(input);
        //        else
        //        {
        //            var res = contactInfoManager.insert(input, companyId);
        //        }
        //        return RedirectToAction("Index");
        //    }

        //    return View(input);
        //}


        public ActionResult Details(int id)
        {
            var result = contactInfoManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Delete(int id)
        {
            contactInfoManager.delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult DropDownList(int? id = null, string propertyName = null, Type propertyType = null, string defaultValue = null, int? propertyTypeId = null)
        {
            //var item = Activator.CreateInstance(modelType);
            ViewBag.propertyName = propertyName;
            if (defaultValue == null)
                defaultValue = "Select One";
            ViewBag.defaultValue = defaultValue;

            ViewBag.selectedItem = id;
            if (propertyType == typeof(CompanyVo))
            {
                ViewBag.items = companyManager.getAll(true);
                ViewBag.idName = "companyId";
            }
            else if (propertyType == typeof(ContactInfoVo))
            {
                ViewBag.items = contactInfoManager.getAll(true);
                ViewBag.idName = "contactInfoId";
            }

            return PartialView("_DropDownList");
        }

		#endregion
    }	 
        
    
}

