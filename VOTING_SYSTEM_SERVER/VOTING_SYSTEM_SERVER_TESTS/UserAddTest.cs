using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VOTING_SYSTEM_SERVER_TESTS.ServiceReference1;

namespace VOTING_SYSTEM_SERVER_TESTS
{
    [TestClass]
    public class UserAddTest
    {
        [TestMethod]
        public void AddUserTest()
        {
            VotingSystemClient client = new VotingSystemClient("BasicHttpBinding_IService1");

            string datauser = "{ Name: \"Иван\", PassportSeries: 222, PassportNumber: 23124, WhoGives: \"me\", WhenGives: \"01.01.2015\", isAdmin: true, Password: 123456, Token: \"\" }";
            
            
            var actual = client.AddUser(datauser);
            
            Assert.AreEqual(null, actual);

        }

        [TestMethod]
        public void LoginTest()
        {
            VotingSystemClient client = new VotingSystemClient("BasicHttpBinding_IService1");

            string datauser = "{ Name: \"Иван\", PassportSeries: 222 }";


            var actual = client.Login(datauser);

            Assert.AreEqual(null, actual);

        }
    }
}
