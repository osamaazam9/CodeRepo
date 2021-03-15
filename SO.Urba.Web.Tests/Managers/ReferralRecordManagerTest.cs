using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SO.Urba.Managers; // added manually
using SO.Urba.Models.ViewModels; // saa
using SO.Urba.Models.ValueObjects; // saa
using SO.Urba.Manager.Models.ViewModels; // was added by "using .. " command


namespace SO.Urba.Web.Tests.Managers
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ReferralRecordManagerTest
    {
        private ReferralRecordManager referralRecordeManager = new ReferralRecordManager();

        /// <summary>
        /// should return:
        ///  
        /// </summary>
        [TestMethod]
        public void getCompanyCategoriesTest()
        {
            List<ReferralVo> result = referralRecordeManager.getReferralsOfMember(34);

            string[,] testArray = new string[,] {
                {"Another Test Categor", "Miosik"},
                {"Dentists", "Western Dental"}
            };
            bool good = true;
            int x = testArray.GetLength(0);
            for (int i = 0; i < 2; ++i)
            {
                good = good && result.ElementAt(i).companyCategoryType.name == testArray[i, 0];
                good = good && result.ElementAt(i).company.name == testArray[i, 1];
            }

            Assert.IsTrue(good == true);
        }

        /// <summary>
        ///  Caterers, iPayment, 01/30/2014
        ///  Caterers, iPayment Inc, 01/30/2014
        ///  Caterers, Jerry's, 01/30/2014
        ///  
        /// </summary>
        [TestMethod]
        public void addTrippleReferralTest()
        {
            ReferralVo rf = new ReferralVo();
            rf.referralDate = DateTime.Now;
            rf.companyCategoryTypeId = 5; // caterers     1; // dentists
            int x = referralRecordeManager.addTrippleReferral(34, rf).Count();

            Assert.IsTrue(true);
        }

        class Person
        {
            public string Name { get; set; }
        }

        class Pet
        {
            public string Name { get; set; }
            public Person Owner { get; set; }
        }

        [TestMethod]
        public void PlayingAroundWithLINQ()
        {
            Person magnus = new Person { Name = "Hedlund, Magnus" };
            Person terry = new Person { Name = "Adams,  John" };
            Person charlotte = new Person { Name = "Weiss, Charlotte" };

            //Pet barley = new Pet { Name = "Barley", Owner = terry };
            //Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            //Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            List<Person> People = new List<Person> { magnus, terry, charlotte };
            //List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };
            List<Pet> Pets = new List<Pet> { whiskers };

            var query = (from person in People
                         join pet in Pets on person equals pet.Owner
                         into tempPets
                         from pets in tempPets.DefaultIfEmpty()
                         select new { OwnerName = person.Name, Pet = pets==null ? "" : pets.Name })
                        .ToList();

            foreach(var x in query)
            {
                string ss = x.ToString();
            }

        }
    }
}
