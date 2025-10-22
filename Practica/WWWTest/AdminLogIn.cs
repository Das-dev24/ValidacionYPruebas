using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SeleniumTests
{
    [TestClass]
    public class AdminLogIn
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheAdminLogInTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44396/LogIn.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys("admin@example.com");
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("Admin123456!");
            driver.FindElement(By.Id("btnLogin")).Click();
            Assert.AreEqual("https://localhost:44396/MainView.aspx", driver.Url);
        }
    }
}
