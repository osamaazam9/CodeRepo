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
    [Authorize(Roles = "Admin")]
    public class MemberRoleTypeController : Controller
    {
        private MemberRoleTypeManager memberRoleTypeManager = new MemberRoleTypeManager();


		public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "System Roles";

            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            { 
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = memberRoleTypeManager.search(input);
                return View(input);
            }
            return View(input);
        }

		

        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = memberRoleTypeManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }
		 
	    #region CRUD

		public ActionResult List()
        {
            var results = memberRoleTypeManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Edit(int id, MemberRoleTypeVo input)
        {

            if (this.ModelState.IsValid)
            {
                var res = memberRoleTypeManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input); 

        }
        public ActionResult Edit(int id)
        {
            var result = memberRoleTypeManager.get(id);
            return View(result); 
        }

        [HttpPost]
        public ActionResult Create(MemberRoleTypeVo input)
        {

            if (this.ModelState.IsValid)
            {

                var item = memberRoleTypeManager.insert(input);
                return RedirectToAction("Index");
            }


            return View(input);

        }

        public ActionResult Create()
        {
            var vo = new MemberRoleTypeVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = memberRoleTypeManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Delete(int id)
        {
            memberRoleTypeManager.delete(id);
            return RedirectToAction("Index");
        }

		#endregion
    }	 
        
    
}

