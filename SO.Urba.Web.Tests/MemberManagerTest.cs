using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SO.Urba.Managers;

namespace SO.Urba.Web.Tests
{
    [TestClass]
    public class MemberManagerTest
    {

        private MemberManager memberManager = new MemberManager();

        [TestMethod]
        public void memberUpdateTest()
        {
            var mem = memberManager.get(4);


            mem.contactInfo.firstName = "zzz";

            memberManager.update(mem);

            Assert.IsTrue(true);



        }
    }
}
