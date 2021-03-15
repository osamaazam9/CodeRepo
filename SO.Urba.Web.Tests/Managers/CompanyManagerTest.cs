using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SO.Urba.Managers;
using SO.Urba.Models.ValueObjects;
using SO.Urba.Models.ViewModels;

namespace SO.Urba.Web.Tests.Managers
{
    [TestClass]
    public class CompanyManagerTest
    {
        private CompanyManager companyManager = new CompanyManager();

        [TestMethod]
        public void getAllOfGivenCategoryTest()
        {
            // 1 is CompanyCategoryTypeId for Dentists
            // 5 for Caterers
            // 10 for Broker
            // 22 for Another Test Category
            List<CompanyVo> results = companyManager.getAllOfGivenCategory(1);

            bool good = true;
            good = good && results[0].score == 55.56m;
            //good = good && results[0].scoreCount == 2;


            Assert.IsTrue(good);
        }

        [TestMethod]
        public void getAllOfGivenCategory2Test()
        {
            CompanySearchFilterVm a = new CompanySearchFilterVm();
            a.isActive = true;
            a.companyCategoryTypeId = 1; // dentists
            CompanySearchFilterVm b = companyManager.contactsByProvider(a);
            // examine input.result 


            Assert.IsTrue(b.result.Count  == 4);
        }
    }
}
