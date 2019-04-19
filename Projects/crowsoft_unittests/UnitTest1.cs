using NUnit.Framework;
using crowsoftmvc.Data;
using Microsoft.Extensions.Configuration;
using crowsoft_unittests;
using crowsoftmvc.Controllers;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private string connection_string;
        private MockupDbContext mockupDbContext;

        /// <summary>
        /// This sets up the test and reads the connectionstring from the appconfig.json file 
        /// </summary>
        [SetUp]
        public void Setup()
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build();

            connection_string = config["CONNECTION_STRING"];

            var optionsBuilder1 = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder1.UseMySql(connection_string);

            mockupDbContext = new MockupDbContext(optionsBuilder1.Options);

        }

        /// <summary>
        /// UserAccountrsController - Test Create and Delete UserAccounts on MySQL database
        /// </summary>
        /// <returns>Pass or Fail</returns>
        [Test]
        public async Task UnitTest_createDeleteSingleUserAccount()
        {
            try
            {
                UserAccountsController userAccountsController = new UserAccountsController(mockupDbContext);

                // Helpers.DataModels is where to generate setup data. 
                UserAccount userAccount = crowsoft_unittests.Helpers.DataModels.GetSingleUser();

                // Create the UserAccount wqill create the User On the Dev MySQL database
                var result = await userAccountsController.Create(userAccount);

                // Get the User Object from the database
                var user = await mockupDbContext.UserAccount.SingleOrDefaultAsync
                    (p => p.EmailAddress == userAccount.EmailAddress);

                bool pass_test = true;

                if (user != null)
                {
                    // Now we need to clean up and delete the record by using the Id returned 
                    var del_result = await userAccountsController.DeleteConfirmed(user.idUserAccount);
                    if (((RedirectToActionResult)del_result).ActionName == "Index")
                    {
                        pass_test = true;
                    }
                    else
                    {
                        pass_test = false;
                    }
                }
                else
                {
                    Assert.Fail("User not created and not found: result: " + result.ToString());
                }

                if (pass_test)
                {
                    // Getting the result back from DB is a Successfull Create
                    Assert.AreEqual(((RedirectResult)result).Url, "/Home");
                }
                else
                {
                    // This will fail when the NUnit Test user Nunit@Test.ie was not deleted
                    Assert.Fail("Did not delete Unit Test UserUser not found!");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Unit Test Fail with exception: " + ex.Message);
            }

        }

        /// <summary>
        /// UserAccountrsController - Test Index by getting a list of user returned from database
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UnitTest_getUserAccountList()
        {
            try
            {
                UserAccountsController userAccountsController = new UserAccountsController(mockupDbContext);

                var userList = await userAccountsController.Index();
                List<UserAccount> users = (List<UserAccount>)((ViewResult)userList).Model;

                Assert.GreaterOrEqual(users.Count, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unit Test Fail with exception: " + ex.Message);
            }

        }

        
    }
}
    