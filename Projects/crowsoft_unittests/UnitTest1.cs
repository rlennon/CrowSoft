using NUnit.Framework;
using System.Configuration;
using System.Collections.Generic;
using crowsoftmvc.Data;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    public class Tests
    {
        private string connection_string;

        // This sets up the test and reads the connectionstring from the appconfig.json file
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build();

            connection_string = config["CONNECTION_STRING"];
        }

        // This is a example test, that test if Dummy records are available in the MySQL Database
        [Test]
        public void Test_GetDummyList()
        {
            DummyContext context = new DummyContext(connection_string);
            List<crowsoftmvc.Models.Dummy> myDummyList = context.GetAllDummys();

            Assert.Greater(myDummyList.Count, 0, "Error No Dummy Records Returned");
        }
    }
}