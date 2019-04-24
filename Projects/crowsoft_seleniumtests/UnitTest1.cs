using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace Tests
{
    public class Tests
    {
        private string webapp_url;
        private string chrome_path;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build();
            
            webapp_url = config["WEB_URL"];
            chrome_path = config["CHROME_DRIVER_PATH"];
        }

        // This Test method will test if the web site can be opened and click on the Register button
        [Test]
        public void SeleniumTest_websiteRegisterClick()
        {
            try
            {
                // Instatiate web driver for Selenium and use the Chrome Drive
                //ChromerDriver driver = new ChromeDriver(chrome_path);
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                IWebDriver driver = new ChromeDriver(chrome_path, options);
                
                // Navigate to the CrowSoft Dev server. Note the webapp_url are in the appconfig.json file
                driver.Navigate().GoToUrl(webapp_url);
                //Console.WriteLine("Opened web application successfully");
                // Test to click on the Register button
                driver.FindElement(By.Id("register")).Click();
                //Console.WriteLine("Navigate to the Register page successfully");
            }
            catch(Exception ex)
            {
                throw ex;               
            }
        }
    }
}