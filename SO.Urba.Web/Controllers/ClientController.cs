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
    public class ClientController : Controller
    {
        private ClientManager clientManager = new ClientManager();
        private ClientOrganizationLookupManager clientOrganizationLookupManager = new ClientOrganizationLookupManager();
        private OrganizationManager organizationManager = new OrganizationManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();

        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Clients";

            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            { 
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = clientManager.search(input);
                return View(input);
            }
            return View(input);
        }


        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = clientManager.clientListExport(input);
                var file = ImportExportHelper.exportToCsv(input.result, false);

                return File(file.FullName, "Application/octet-stream", file.Name);
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
            if (modelType == typeof(OrganizationVo))
            {
                ViewBag.items = organizationManager.getAll(true);
                ViewBag.idName = "organizationId";
                return PartialView("_MultySelectDropDownList");
            }

            return PartialView("_DropDownList");
        }

	    #region CRUD

		public ActionResult List()
        {
            var results = clientManager.getAll(null);
            return PartialView(results);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, ClientVo input)
        {
            bool foundTheMatch = false;
            
            if (this.ModelState.IsValid)
            {

                ClientVo item = clientManager.get(id);
                if (item.organizations != null)
                {
                    foreach (ClientOrganizationLookupVo orgLookupVo in item.clientOrganizationLookupses)
                    {
                        foundTheMatch = false;
                        foreach (int clientRoleId in input.organizations)
                        {
                            if (orgLookupVo.organizationId == clientRoleId)
                            {
                                input.organizations.Remove(clientRoleId);
                                foundTheMatch = true;
                                break;
                            }
                        }
                        if (!foundTheMatch)
                            clientOrganizationLookupManager.delete(orgLookupVo.clientOrganizationLookupId);
                    }
                }
                if (input.organizations != null)
                {
                    foreach (int orgId in input.organizations)
                    {
                        var clientOrganizationLookupVo = new ClientOrganizationLookupVo();
                        clientOrganizationLookupVo.clientId = input.clientId;
                        clientOrganizationLookupVo.organizationId = orgId;
                        clientOrganizationLookupVo.isActive = true;
                        clientOrganizationLookupManager.insert(clientOrganizationLookupVo);
/*=================This sections adds clientfee amount to the organization total fee amount=========================================*/
                        if (input.feeAmount != null)
                        {
                            OrganizationVo currentOrg = organizationManager.get(orgId);
                            if (currentOrg != null)
                            {
                                OrganizationManager.updateOrganizationFeeAmountForClientController(currentOrg, input.feeAmount);
                            }
                        }

                    }
                }
               
                contactInfoManager.update(input.contactInfo, input.contactInfoId);

                var res = clientManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input); 

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.totalAmountForFinalPayments = ReferralManager.addAllFinalPayments(id);
            ViewBag.currentYear = DateTime.Now.Year;
            var result = clientManager.get(id);
            if (result.organizations.Count > 0)
                result.organizations = new List<int>();
            if (result.clientOrganizationLookupses != null)
            {
                foreach (var item in result.clientOrganizationLookupses)
                {
                    result.organizations.Add((int)item.organizationId);
                }
            }
            return View(result); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ClientVo input)
        {
            ViewBag.Title = "Add New Client";

            if (this.ModelState.IsValid)
            {

                if ( input.contactInfo != null && input.contactInfo.state != null)
                    input.contactInfo.state = input.contactInfo.state.ToUpper();

                var item = clientManager.insert(input);
                if (input.organizations != null)
                {
                    foreach (int orgId in input.organizations)
                    {
                        var clientOrganizationLookupVo = new ClientOrganizationLookupVo();
                        clientOrganizationLookupVo.clientId = input.clientId;
                        clientOrganizationLookupVo.organizationId = orgId;
                        clientOrganizationLookupVo.isActive = true;

                        clientOrganizationLookupManager.insert(clientOrganizationLookupVo);
 /*=================This sections adds clientfee amount to the organization total fee amount=========================================*/
                        if (input.feeAmount != null)
                        {
                            OrganizationVo currentOrg = organizationManager.get(orgId);
                            if (currentOrg != null)
                            {
                                OrganizationManager.updateOrganizationFeeAmountForClientController(currentOrg, input.feeAmount);
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }


            return View(input);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var vo = new ClientVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = clientManager.get(id);
            ViewBag.totalAmountForFinalPayments = ReferralManager.addAllFinalPayments(id);
            ViewBag.currentYear = DateTime.Now.Year;
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            contactInfoManager.delete((int)clientManager.get(id).contactInfoId);
            clientManager.delete(id);
            return RedirectToAction("Index");
        }

		#endregion
    }	 
        
    
}

