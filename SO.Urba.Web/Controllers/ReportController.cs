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
using SO.Urba.Manager.Models.ViewModels;

namespace SO.Urba.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {

        private CompanyManager companyManager = new CompanyManager();
        private ReportManager reportManager = new ReportManager();
        private ClientManager clientManager = new ClientManager();
        private CompanyCategoryTypeManager copanyCategoryTypeManager = new CompanyCategoryTypeManager();

        public ActionResult List()
        {
            var results = companyManager.getAll(null);
            return PartialView(results);
        }

        public ActionResult Index(ReportSearchFilterVm input = null, Paging paging = null)
        {
            if (input == null) input = new ReportSearchFilterVm();
            input.paging = paging;

            return View(input);
        }

        // adjust dates not to show time
        public FileResult CategoryReferralByDate(ReportSearchFilterVm input = null)
        {
            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = reportManager.categoryReferralByDate(input);
                var file = ImportExportHelper.exportToCsv(input.result, true);

                // alert, if the only object is the info about Date period
                if (input.result.Count() != 0) 
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }

            return null;
        }

        // fix
        public FileResult CategorySatisfactionByDate(ReportSearchFilterVm input = null)
        {
            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = reportManager.categorySatisfactionByDate(input);
                var file = ImportExportHelper.exportToCsv(input.result, true);

               // if (file != null)
                if (input.result.Count() != 0) 
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }

            return null;
        }

        //  working
        public FileResult ClientReferralByDate(ReportSearchFilterVm input = null)
        {
            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = reportManager.clientReferralByDate(input);
                var file = ImportExportHelper.exportToCsv(input.result, false);

                if (input.result.Count() != 0) 
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }

            return null;
        }

        public FileResult ProdiverReferralByDate(ReportSearchFilterVm input = null)
        {
            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = reportManager.prodiverReferralByDate(input);
                var file = ImportExportHelper.exportToCsv(input.result, false);

                if (input.result.Count() != 0) 
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }

            return null;
        }

        
        public ActionResult ClientDropDownList(int? id = null)
        {
            var items = clientManager.getAll();
            List<SelectListItem> clientNameList = new List<SelectListItem>();
            clientNameList.Add(new SelectListItem { Text = "Clients", Value = "" });

            foreach (var item in items)
            {
                var itemValue = item.clientId.ToString();
                var itemName = item.contactInfo.fullname;
                clientNameList.Add(new SelectListItem { Text = itemName, Value = itemValue });
            }
            //ViewData["clientId"] = new SelectList(clientNameList, "value", "text");
            ViewBag.items = clientNameList;

            return PartialView("_ClientDropDownList");
        }
        // Category Referral By Date Feature ------------------------------------
        public ActionResult SpecificCategoryReferralsByDate(SpecificCategoryReferralsByDateVM categoryModel)
        {
            return PartialView("_SpecificCategoryReferralsByDate", categoryModel);
        }
        // working on this 
        public FileResult SpecificCategoryReferralsByDateFile(SpecificCategoryReferralsByDateVM input)
        {
            if (ModelState.IsValid)
            {
                input = reportManager.SpecificCategoryReferralsByDateFileSearch(input);
                var file = ImportExportHelper.exportToCsv(input.result, true);

                // alert, if the only object is the info about Date period
                if (input.result.Count() != 0)
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }
            else
            {
                this.Response.Write("<script> alert('Plese input valid dates!'); window.history.back(); </script>");
            }
            return null;
        }
        // Category Referral By Date Feature Finish ------------------------------------

        // All Referrals For a Given Date Feature ------------------------------------
        public ActionResult AllReferralsByDate(AllReferralsByDateVM allReferralsByDateModel)
        {
            return PartialView("_AllReferralsByDate", allReferralsByDateModel);

        }
        public FileResult AllReferralsByDateFile(AllReferralsByDateVM input)
        {
            if (ModelState.IsValid)
            {
                input = reportManager.AllReferralsByDateFileSearch(input);
                var file = ImportExportHelper.exportToCsv(input.allReferrals, true);

                // alert, if the only object is the info about Date period
                if (input.allReferrals.Count() != 0)
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }
            else
            {
                this.Response.Write("<script> alert('Plese input valid dates!'); window.history.back(); </script>");
            }
            return null;
        }
        //All Referrals For a Given Date Feature Finish---------------------------------------------------------

        //All Referrals for a client for a given time period ---------------------------------------------------
        public ActionResult AllReferralsForaClient(AllReferralsForaClientVM AllReferralsForaClientDateModel)
        {
            return PartialView("_AllReferralsForaClient", AllReferralsForaClientDateModel);

        }
        public FileResult AllReferralsForaClientFile(AllReferralsForaClientVM input)
        {
            if (ModelState.IsValid)
            {
                input = reportManager.AllReferralsForaClientFileSearch(input);
                var file = ImportExportHelper.exportToCsv(input.allReferralsForaClient, true);

                // alert, if the only object is the info about Date period
                if (input.allReferralsForaClient.Count() != 0)
                    return File(file.FullName, "Application/octet-stream", file.Name);
                else
                    this.Response.Write("<script> alert('No Data found based on the criteria!'); window.history.back(); </script>");
            }
            else
            {
                this.Response.Write("<script> alert('Plese input valid dates!'); window.history.back(); </script>");
            }
            return null;
        }
        //All Referrals for a client for a given time period Finish ---------------------------------------------
    }
}