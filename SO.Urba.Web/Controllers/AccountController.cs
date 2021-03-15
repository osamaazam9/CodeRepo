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

namespace SO.Urba.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        MemberManager memberManager = new MemberManager();
        MemberRoleLookupManager memberRoleLookupManager = new MemberRoleLookupManager();
        ContactInfoManager contactInfoManager = new ContactInfoManager();
        MemberRoleTypeManager memberRoleTypeManager = new MemberRoleTypeManager();

        [HttpPost]
        public ActionResult Login(MemberVo input)
        {

            if (this.ModelState.IsValid)
            {
                if (CurrentMember.validateUser(input.username, input.password))
                {
                    FormsAuthentication.SetAuthCookie(input.username, true);
                    return RedirectToAction("Index", "Home");
                    //FormsAuthentication.RedirectFromLoginPage(input.username, true);
                }
                else
                {
                    ViewBag.uname = input.username;
                    this.ModelState.AddModelError("", "Please check Username and Password, and try again.");
                }
            }

            return View();
        }

        public ActionResult Login()
        {
            var login = new MemberVo();
            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            CurrentMember.member = null;

            return RedirectToAction("Login", "Account");
        }

        public ActionResult DropDownList(int? id = null, string propertyName = null, Type modelType = null, string defaultValue = null)
        {
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

        [Authorize]
        public ActionResult Profile()
        {
            var result = CurrentMember.member;
            return View("Details", result);
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            var result = CurrentMember.member;
            if (result.memberRoleTypes.Count > 0)
                result.memberRoleTypes = new List<int>();
            if (result.memberRoleLookupses != null)
            {
                foreach (var item in result.memberRoleLookupses)
                {
                    result.memberRoleTypes.Add((int)item.memberRoleTypeId);
                }
            }
            return View("Edit", result);
        }
        [HttpPost]
        public ActionResult EditProfile(MemberVo input)
        {

            Edit(input);
            if (!this.ModelState.IsValid)
                return View("Edit", input);
            CurrentMember.reload();
            return View("Details", CurrentMember.member);
        }


        [Authorize]
        public ActionResult Edit(MemberVo input)
        {
            var id = CurrentMember.member.memberId;
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
    }
}