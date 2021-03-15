using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
     [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        private MemberManager memberManager = new MemberManager();
        private MemberRoleTypeManager memberRoleTypeManager = new MemberRoleTypeManager();
        private MemberRoleLookupManager memberRoleLookupManager = new MemberRoleLookupManager();
        private ContactInfoManager contactInfoManager = new ContactInfoManager();

     
        public ActionResult Index(SearchFilterVm input = null, Paging paging = null)
        {
            ViewBag.Title = "Manage Users";

            if (input == null) input = new SearchFilterVm();
            input.paging = paging;

            if (this.ModelState.IsValid)
            {
                if (input.submitButton != null)
                    input.paging.pageNumber = 1;
                input = memberManager.search(input);
                return View(input);
            }
            return View(input);
        }



        public FileResult Export(SearchFilterVm input = null)
        {

            if (this.ModelState.IsValid)
            {
                input.paging = null;
                input = memberManager.search(input);
                var file = ImportExportHelper.exportToCsv(input.result);

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
            if (modelType == typeof(MemberRoleTypeVo))
            {
                ViewBag.items = memberRoleTypeManager.getAll(true);
                ViewBag.idName = "memberRoleTypeId";
                return PartialView("_MultySelectDropDownList");
            }

            return PartialView("_DropDownList");
        }

       
       
         #region CRUD

        public ActionResult List()
        {
            var results = memberManager.getAll(null);
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult Edit(int id, MemberVo input)
        {
            bool foundTheMatch = false;
            MemberVo item = memberManager.get(id);
            if (this.ModelState.IsValid)
            {
                if (item.memberRoleTypes != null)
                {
                    foreach (MemberRoleLookupVo roleLookupVo in item.memberRoleLookupses)
                    {
                        foundTheMatch = false;
                        foreach (int memberRoleId in input.memberRoleTypes)
                        {
                            if (roleLookupVo.memberRoleTypeId == memberRoleId)
                            {
                                input.memberRoleTypes.Remove(memberRoleId);
                                foundTheMatch = true;
                                break;
                            }
                        }
                        if (!foundTheMatch)
                            memberRoleLookupManager.delete(roleLookupVo.memberRoleId);
                    }
                }
                if (input.memberRoleTypes != null)
                {
                    
                    foreach (int roleId in input.memberRoleTypes)
                    {
                        var memberRoleLookupVo = new MemberRoleLookupVo();
                        memberRoleLookupVo.memberId = input.memberId;
                        memberRoleLookupVo.memberRoleTypeId = roleId;
                        memberRoleLookupVo.isActive = true;

                        memberRoleLookupManager.insert(memberRoleLookupVo);

                    }

                }
                contactInfoManager.update(input.contactInfo, input.contactInfo.contactInfoId);
                var res = memberManager.update(input, id);
                return RedirectToAction("Index");
            }

            return View(input);

        }
        public ActionResult Edit(int id)
        {
            var result = memberManager.get(id);
            if (result.memberRoleTypes.Count > 0)
                result.memberRoleTypes = new List<int>();
            if (result.memberRoleLookupses != null)
            {
                foreach (var item in result.memberRoleLookupses)
                {
                    result.memberRoleTypes.Add((int)item.memberRoleTypeId);
                }
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult Create(MemberVo input)
        {
            ViewBag.Title = "Add New User";

            if (this.ModelState.IsValid)
            {
                input.password = CurrentMember.HashWord(input.password);

                var item = memberManager.insert(input);

                if (input.memberRoleTypes != null)
                {
                    foreach (int roleId in input.memberRoleTypes)
                    {
                        var memberRoleLookupVo = new MemberRoleLookupVo();
                        memberRoleLookupVo.memberId = input.memberId;
                        memberRoleLookupVo.memberRoleTypeId = roleId;
                        memberRoleLookupVo.isActive = true;

                        memberRoleLookupManager.insert(memberRoleLookupVo);

                    }
                }
                return RedirectToAction("Index");
            }


            return View(input);

        }

        public ActionResult Create()
        {
            var vo = new MemberVo();
            return View(vo);
        }

        public ActionResult Details(int id)
        {
            var result = memberManager.get(id);
            return View(result);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Delete(int id)
        {
           
            contactInfoManager.delete((int)memberManager.get(id).contactInfoId);
            memberManager.delete(id);
            return RedirectToAction("Index");
        }

        #endregion
    }


}

