using SO.Urba.Managers;
using SO.Utility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SO.Urba.Web.Controllers
{
    [Authorize]
    public class DropDownController : Controller
    {
        private CompanyCategoryTypeManager companyCategoryTypeManager = new CompanyCategoryTypeManager();
        private CompanyManager companyManager = new CompanyManager();
        private ClientManager clientManager = new ClientManager();
         /// <summary>
        /// /DropDown/companyCategoryTypes
        /// 
        ///   @Html.Action("companyCategoryTypes", "DropDown", new { id = Model.companyCategoryTypeId })
         /// </summary>
        public ActionResult companyCategoryTypes(int? id = null)
        {
            var vo = new DropDownVm();
            vo.propertyName = "companyCategoryTypeId";
            vo.dataValueField = "companyCategoryTypeId";
            vo.dataTextField = "name";
            vo.selectedValue = id;
            vo.items = companyCategoryTypeManager.getAll(true); // CompanyCategoryTypeVo

            return View("_DropDownList", vo);
        }
        /// <summary>
        ///  /DropDown/companyCategoryTypes2
        ///  
         /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult companyCategoryTypes2(int? id = null)
        {
            var vo = new DropDownVm();
            vo.propertyName = "companyCategoryTypeId";
            vo.dataValueField = "companyCategoryTypeId";
            vo.dataTextField = "name";
            vo.selectedValue = id;
            vo.optionLabel = "All Providers";
            vo.items = companyCategoryTypeManager.getCompanyCategories(true); // CompanyCategoryClass

            return View("_DropDownList", vo);
        }

        /// <summary>
        /// /DropDown/companies
        /// 
        ///   @Html.Action("companies", "DropDown", new { id = Model.companyId })
        /// </summary>
        public ActionResult companies(int? id = null)
        {
            var vo = new DropDownVm();
            vo.propertyName = "companyId";
            vo.dataValueField = "companyId";
            vo.dataTextField = "companyName";
            vo.selectedValue = id;
            vo.optionLabel = "Providers";
            vo.items = companyManager.getAll(true); // CompanyCategoryTypeVo

            return View("_DropDownList", vo);
        }
        /// <summary>
        /// /DropDown/clients
        /// 
        ///   @Html.Action("clients", "DropDown", new { id = Model.clientId })
        /// </summary>
        public ActionResult clients(int? id = null)
        {
            var vo = new DropDownVm();
            vo.propertyName = "clientId";
            vo.dataValueField = "clientId";
            vo.dataTextField = "clientId";
            vo.selectedValue = id;
            vo.optionLabel = "All Clients";
            vo.items = clientManager.getAll(true);  // CompanyCategoryTypeVo

            return View("_DropDownList", vo);
        }
    }
}