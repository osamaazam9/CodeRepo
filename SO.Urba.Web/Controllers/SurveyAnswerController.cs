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

namespace SO.Urba.Controllers
{
    [Authorize]
    public class SurveyAnswerController : Controller
    {
        private SurveyAnswerManager surveyAnswerManager = new SurveyAnswerManager();
        private ReferralManager referralManager = new ReferralManager();

        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = surveyAnswerManager.search(input);
                return View(input);
            }
            return View(input);
        }



        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = surveyAnswerManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

                return File(file.FullName, "Application/octet-stream", file.Name);
            }

            return null;
        }

        public ActionResult SurveyDetails(int id)
        {
            var result = referralManager.get(id);
            ViewBag.returnUrl = Url.Action("Index","ReferralRecord", new { id = result.clientId, companyCategoryTypeId = result.companyCategoryTypeId });
            return View(new SurveyAnswerVm(result.surveyAnswerses, result.surveyComment));
        }

        #region CRUD

        public ActionResult List()
        {
            var results = surveyAnswerManager.getAll(null);
            return PartialView(results);
        }
        
        [HttpPost]
        public ActionResult Edit(SurveyAnswerVm input)
        {

            if (this.ModelState.IsValid)
            {
                foreach (var item in input.voSlist)
                {
                    if (item.answer != null)
                        surveyAnswerManager.update(item,item.surveyAnswerId);
                }
                var referral = referralManager.get(input.voSlist[0].referralId);
                referral.surveyComment = input.comment;
                referralManager.update(referral);
                return RedirectToAction("Index", "ReferralRecord", new { id = referral.clientId, companyCategoryTypeId = referral.companyCategoryTypeId });
            }

            return View(input);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var result = referralManager.get(id);
            ViewBag.returnUrl = Url.Action("Index", "ReferralRecord", new { id = result.clientId, companyCategoryTypeId = result.companyCategoryTypeId });
            return View(new SurveyAnswerVm(result.surveyAnswerses, result.surveyComment));

        }

        [HttpPost]
        public ActionResult Create(SurveyAnswerVm input)
        {

            if (this.ModelState.IsValid)
            {
                foreach (var item in input.voSlist)
                {
                    if (item.answer != null)
                        surveyAnswerManager.insert(item);
                }
                var referral = referralManager.get(input.voSlist[0].referralId);
                referral.surveyComment = input.comment;
                referralManager.update(referral);
                return RedirectToAction("Index","ReferralRecord", new { id = referral.clientId, companyCategoryTypeId = referral.companyCategoryTypeId });
            }


            return View(input);

        }

        public ActionResult Create(int referralId)
        {
            SurveyAnswerVm vm = new SurveyAnswerVm(referralId);
            var referral = referralManager.get(referralId);
            ViewBag.returnUrl = Url.Action("Index", "ReferralRecord", new { id = referral.clientId, companyCategoryTypeId = referral.companyCategoryTypeId });

            return View(vm);
        }

        //public ActionResult Details(Guid id)
        //{
        //    var result = surveyAnswerManager.get(id);
        //    return View(result);
        //}


        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        //public ActionResult Delete(Guid id)
        //{
        //    surveyAnswerManager.delete(id);
        //    return RedirectToAction("Index");
        //}

        #endregion
    }


}

