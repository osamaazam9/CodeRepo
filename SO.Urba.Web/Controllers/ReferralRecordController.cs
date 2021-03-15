using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SO.Urba.Manager.Models.ViewModels;
using SO.Urba.Managers;
using SO.Urba.Manager;
using SO.Urba.Models.ValueObjects;
using SO.Utility.Models.ViewModels;
using SO.Urba.Models.ViewModels;

namespace SO.Urba.Web.Controllers
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public sealed class NoCacheAttribute : ActionFilterAttribute
    //{
    //    public override void OnResultExecuting(ResultExecutingContext filterContext)
    //    {
    //        filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    //        filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
    //        filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    //        filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        filterContext.HttpContext.Response.Cache.SetNoStore();

    //        base.OnResultExecuting(filterContext);
    //    }
    //}
    [Authorize]
    public class ReferralRecordController : Controller
    {
        private CompanyCategoryTypeManager companyCategoryTypeManager = new CompanyCategoryTypeManager();
        private ReferralRecordManager referralRecordManager = new ReferralRecordManager();
        private CompanyManager companyManager = new CompanyManager();
        private ReferralManager referralManager = new ReferralManager();
        private ClientManager clientManager = new ClientManager();
        private NotificationManager notificationManager = new NotificationManager();
        private OrganizationManager orgManager = new OrganizationManager();

        //
        // GET: /ReferralRecord/
        public ActionResult Index(int? id = null, int? companyCategoryTypeId = null)
        {
            ReferralRecordVm input = new ReferralRecordVm();
            //  input.companyCategoryType = companyCategoryTypeManager.getCompanyCategories();
            input.clientId = id.GetValueOrDefault();
            input.companyCategoryTypeId = companyCategoryTypeId.GetValueOrDefault();
            return View(input);
        }
        // POST: /ReferralRecord/
        [HttpPost]
        public ActionResult Index(ReferralRecordVm input, int? id = null)
        {
            // input.companyCategoryType = companyCategoryTypeManager.getCompanyCategories();
            return View(input);
        }
        public ActionResult ReferralHistory(int? id = null, ProviderReferralHistoryFilter referralHistoryFilter = ProviderReferralHistoryFilter.All)
        {
            // id => clientId
            ViewBag.id = id;
            ViewBag.referralHistoryFilter = referralHistoryFilter;

            List<ReferralVo> data = referralRecordManager.getReferralsOfMember(id, true, referralHistoryFilter);
            ViewBag.totalAmountForFinalPayments = ReferralManager.addAllFinalPayments(id);
            ViewBag.currentYear = DateTime.Now.Year;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ReferralHistory", data);
            }
            else
            {
                return View("_ReferralHistory", data);
            }
        }

        public ActionResult AddSingleReferral(int id, int coId, int companyCategoryTypeId, int dlgType)
        {
            ViewBag.ModalDialogTitle = "Add a referral";
            // id => clientId
            ReferralVo newRef = new ReferralVo();
            newRef.referralDate = DateTime.Now;
            newRef.clientId = id;
            newRef.companyId = coId;
            newRef.companyCategoryTypeId = companyCategoryTypeId;
            newRef.dlgType = dlgType;
            return PartialView("_EditReferralModal", newRef);
        }

        [HttpPost]
        public ActionResult AddSingleReferral(ReferralVo referral, int companyCategoryTypeId)
        {
            if (this.ModelState.IsValid)
            {
                referral.companyCategoryTypeId = companyCategoryTypeId;
                ReferralVo r = referralManager.insert(referral);
                notificationManager.sendReferralNotification(new int[] { referral.referralId });
                return View("CloseModalsAndRefresh");
            }
            return PartialView("_EditReferralModal", referral);
        }




        public ActionResult TripleReferral(int id, int companyCategoryTypeId)
        {
            ViewBag.ModalDialogTitle = "Add triple referral";
            // id => clientId
            ReferralVo newRefFilter = new ReferralVo();
            newRefFilter.referralDate = DateTime.Now;
            newRefFilter.clientId = id;
            return PartialView("_EditReferralModal", newRefFilter);
        }
        [HttpPost]
        public ActionResult TripleReferral(int id, int companyCategoryTypeId, ReferralVo referral)
        {
            if (this.ModelState.IsValid)
            {
                // id => clientId
                referral.companyCategoryTypeId = companyCategoryTypeId;
                var referrals = referralRecordManager.addTrippleReferral(id, referral, true);
                notificationManager.sendReferralNotification(referrals);
                return View("CloseModalsAndRefresh");
            }
            return PartialView("_EditReferralModal", referral);
        }



        // Direct edit of given referral
        public ActionResult EditReferralModal(int referralId)
        {
            ViewBag.ModalDialogTitle = "Edit referral";
            ReferralVo referral = referralManager.get(referralId);
            return PartialView("_EditReferralModal", referral);
        }
        [HttpPost]
        public ActionResult EditReferralModal(int referralId, ReferralVo referral)
        {
            ReferralVo item = referralManager.get(referralId);
            decimal? oldFinalPayment = item.finalQuote;
            if (this.ModelState.IsValid)
            {
                if (item.referralId > 0)
                {
                    item.description = referral.description;
                    item.quote = referral.quote;
                    item.referralDate = referral.referralDate;
                    item.accepted = referral.accepted;
                    item.finalQuote = referral.finalQuote;
                    item.finishDate = referral.finishDate;
                    item.commissionAmount = referral.commissionAmount;
                    item.commissionDatePaid = referral.commissionDatePaid;
                    ReferralVo r = referralManager.update(item, referralId);
                    int orgID = OrganizationManager.getOrgIDForClient(item.clientId);
                    OrganizationVo currentOrg = orgManager.get(orgID);
                    if (currentOrg != null)
                    {OrganizationManager.addAllFinalPaymentsForMembertoOrganizationFeeAmount(currentOrg, item.finalQuote, oldFinalPayment);}
                    //notificationManager.sendReferralNotification(new int[] { referralId }, true);
                    return View("CloseModalsAndRefresh");
                }
            }
            return PartialView("_EditReferralModal", referral);
        }




        public ActionResult ProvidersAvailable(int id, int companyCategoryTypeId)
        {
            List<CompanyVo> companies = companyManager.getAllOfGivenCategory(companyCategoryTypeId);
            if (companies.Count() == 0)
                TempData["trippleDisabled"] = true;
            else
                TempData["trippleDisabled"] = false;

            ViewBag.clientId = id;
            ViewBag.companyCategoryTypeId = companyCategoryTypeId;
            return View("_ProvidersAvailable", companies);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int referralId, int id, int companyCategoryTypeId)
        {
            referralManager.delete(referralId);
            return RedirectToAction("Index", new { id = id, companyCategoryTypeId = companyCategoryTypeId });
        }

        public ActionResult CompanyCategoriesDropDown(int? companyCategoryTypeId = null)
        {
            var vo = new DropDownVm();
            vo.propertyName = "companyCategoryTypeId";
            vo.dataValueField = "companyCategoryTypeId";
            vo.dataTextField = "name";
            vo.selectedValue = companyCategoryTypeId;
            vo.optionLabel = "NONE";
            vo.items = companyCategoryTypeManager.getCompanyCategories(true); // CompanyCategoryClass

            return View("_CompanyCategoriesDropDown", vo);

        }

        public ActionResult ClientInfoOnReferralRecord(int id)
        {
            if (id != 0)
            {
                ClientVo client = clientManager.get(id);
                return View("_ClientInfoOnReferralRecord", client);
            }
            else { return null; }
        }
        public ActionResult CloseModals()
        {
            return View();
        }
        public ActionResult CloseModalsAndRefresh()
        {
            return View();
        }
    }
}