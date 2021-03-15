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
      [Authorize]
    public class CompanyCategoryLookupController : Controller
    {
        private CompanyCategoryLookupManager companyCategoryLookupManager = new CompanyCategoryLookupManager();


		public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            { 
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = companyCategoryLookupManager.search(input);
                return View(input);
            }
            return View(input);
        }

		

        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = companyCategoryLookupManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }
		 
	    #region CRUD

		public ActionResult List()
        {
            var results = companyCategoryLookupManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, CompanyCategoryLookupVo input)
        {

            if (this.ModelState.IsValid)
            {
                var res = companyCategoryLookupManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input); 

        }
        public ActionResult Edit(Guid id)
        {
            var result = companyCategoryLookupManager.get(id);
            return View(result); 
        }

        [HttpPost]
        public ActionResult Create(CompanyCategoryLookupVo input)
        {

            if (this.ModelState.IsValid)
            {

                var item = companyCategoryLookupManager.insert(input);
                return RedirectToAction("Index");
            }


            return View(input);

        }

        public ActionResult Create()
        {
            var vo = new CompanyCategoryLookupVo();
            return View(vo);
        }

        public ActionResult Details(Guid id)
        {
            var result = companyCategoryLookupManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Delete(Guid id)
        {
            companyCategoryLookupManager.delete(id);
            return RedirectToAction("Index");
        }

		#endregion
    }	 
        
    
}

