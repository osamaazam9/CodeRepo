using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SO.Urba.Managers; // added by me
using SO.Urba.Models.ViewModels; // saa
using SO.Urba.Models.ValueObjects; // saa
using SO.Urba.Manager.Models.ViewModels; // was added by "using .. " command
/*
 * namespace
namespece SO.Urba.Models.ViewModels
namespace SO.Urba.Models.ValueObjects
 * 
 * folder
 SO.Urba.Manager.Models.ValueObjects
 SO.Urba.Manager.Models.ViewModels
*/
namespace SO.Urba.Web.Tests.Managers
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CompanyCategoryTypeManagerTest
    {
        private CompanyCategoryTypeManager companyCategoryTypeManager = new CompanyCategoryTypeManager();
        
        /* - OLD
        /// <summary>
        /// should return:
        /// 
        /// Another Test Categor, 1 provider
        /// BailBonds, 1 provider
        /// Broker, 1 provider
        /// Caterers, 5 providers
        /// Cleaners, 1 provider
        /// Dentists, 4 providers
        /// Dusters, 3 providers
        /// EconomicDevlopment, 2 providers
        /// Engineer-Civil, 1 provider
        /// Engineer-Mechanical, 1 provider
        /// Engineer-Structural, 1 provider
        /// Plumbers, 1 provider
        /// Printing, 1 provider
        /// Programmers, 7 providers
        /// RealEstate, 4 providers
        /// TestCategory, 0 provider
        /// </summary>
        [TestMethod]
        public void getCompanyCategoriesTest_Old_with_Counts()
        {
            List<CompanyCategoryTypeVo> result = companyCategoryTypeManager.getCompanyCategories();
            //List<CompanyCategoryTypeVo > result = companyCategoryTypeManager.getCompanyCategories();
            string[,] testArray = new string[,] {
                {"Another Test Categor", "1"},
                {"BailBonds", "1"},
                {"Broker", "1"},
                {"Caterers", "5"},
                {"Cleaners", "1"},
                {"Dentists", "4"},
                {"Dusters", "3"},
                {"EconomicDevlopment", "2"},
                {"Engineer-Civil", "1"},
                {"Engineer-Mechanical", "1"},
                {"Engineer-Structural", "1"},
                {"Plumbers", "1"},
                {"Printing", "1"},
                {"Programmers", "7"},
                {"RealEstate", "4"},
                {"TestCategory", "0"}
            };
            bool good  = true;
            for(int i = 0; i<testArray.Length; ++i)
            {
                good = good && result.ElementAt(i).name == testArray[i, 0];
                good = good && result.ElementAt(i).companyCategoryCount.ToString() == testArray[i, 1];
            }

            Assert.IsTrue(good == true);
        }
        */

        /// <summary>
        /// should return:
        /// 
        /// Another Test Categor
        /// BailBonds
        /// Broker
        /// Caterers
        /// Cleaners
        /// Dentists
        /// Dusters
        /// EconomicDevlopment
        /// Engineer-Civil
        /// Engineer-Mechanical
        /// Engineer-Structural
        /// Plumbers
        /// Printing
        /// Programmers
        /// RealEstate
        /// TestCategory
        /// </summary>
        [TestMethod]
        public void getCompanyCategoriesTest()
        {
            List<CompanyCategoryTypeVo> result = companyCategoryTypeManager.getCompanyCategories();
            //List<CompanyCategoryTypeVo > result = companyCategoryTypeManager.getCompanyCategories();
            HashSet<string> testSet = new HashSet<string>();
            testSet.Add("Another Test Categor");
            testSet.Add("BailBonds");
            testSet.Add("Broker");
            testSet.Add("Caterers");
            testSet.Add("Cleaners");
            testSet.Add("Dentists");
            testSet.Add("Dusters");
            testSet.Add("EconomicDevlopment");
            testSet.Add("Engineer-Civil");
            testSet.Add("Engineer-Mechanical");
            testSet.Add("Engineer-Structural");
            testSet.Add("Plumbers");
            testSet.Add("Printing");
            testSet.Add("Programmers");
            testSet.Add("RealEstate");
            testSet.Add("TestCategory");

            bool good = result.Count == testSet.Count;
            foreach (var companyCategoryType in result)
            {
                if (!(good = good && testSet.Contains(companyCategoryType.name)))
                    break;
            }

            Assert.IsTrue(good == true);
        }
    }
}
