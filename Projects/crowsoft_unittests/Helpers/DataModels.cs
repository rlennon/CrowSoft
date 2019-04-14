using crowsoftmvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace crowsoft_unittests.Helpers
{
    public static class DataModels
    {
        public static UserAccount GetSingleUser()
        {
            UserAccount userAccount = new UserAccount();
            userAccount.CompanyName = "Nunit Company";
            userAccount.AddressLine = "Nunit Address Line";
            userAccount.Country = "Nunit Ireland";
            userAccount.County = "Nunit Donegal";
            userAccount.DateCreated = DateTime.Now;
            userAccount.EirCode = "NunitF10";
            userAccount.EmailAddress = "Nunit@Test.ie";
            userAccount.FirstName = "Nunit Name";
            userAccount.LastName = "Nunit LName";
            userAccount.TelephoneNo = "123456789";
            userAccount.TypeUser = "Admin";

            return userAccount;
        }

    }
}
