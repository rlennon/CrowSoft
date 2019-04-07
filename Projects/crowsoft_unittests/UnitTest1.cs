using NUnit.Framework;
using System.Configuration;
using System.Collections.Generic;
using crowsoftmvc.Data;

namespace Tests
{
    public class Tests
    {
        private string connection_string;

        [SetUp]
        public void Setup()
        {
            connection_string = ConfigurationManager.AppSettings["ConnectionString"];
        }

        [Test]
        public void Test_GetDummyList()
        {
            //DummyContext context = new DummyContext("server=172.28.25.133;port=3306;database=crowsoftdb;uid=dbadmin;password=csoftsql");
            DummyContext context = new DummyContext(connection_string);

            List<crowsoftmvc.Models.Dummy> myDummyList = context.GetAllDummys();

            Assert.Greater(myDummyList.Count, 0, "Error No Dummy Records Returned");
        }
    }
}