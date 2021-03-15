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

    public class QuestionTypeController : Controller
    {
        private QuestionTypeManager questionTypeManager = new QuestionTypeManager();


		public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Survey Questions";

            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            { 
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = questionTypeManager.search(input);
                return View(input);
            }
            return View(input);
        }

		

        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = questionTypeManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }

        
	    #region CRUD

		public ActionResult List()
        {
            var results = questionTypeManager.getAll(null);
            return PartialView(results);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, QuestionTypeVo input)
        {

            if (this.ModelState.IsValid)
            {
                var res = questionTypeManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input); 

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var result = questionTypeManager.get(id);
            return View(result); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(QuestionTypeVo input)
        {

            if (this.ModelState.IsValid)
            {

                var item = questionTypeManager.insert(input);
                return RedirectToAction("Index");
            }


            return View(input);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var vo = new QuestionTypeVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = questionTypeManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            questionTypeManager.delete(id);
            return RedirectToAction("Index");
        }

		#endregion
    }	 
        
    
}

