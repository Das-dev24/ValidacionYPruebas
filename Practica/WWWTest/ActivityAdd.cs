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
    public class ActivityAdd
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
                //driver.Quit();// quit does not close the window
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
        public void TheActivityAddTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44396/LogIn.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys("admin@example.com");
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("Admin123456!");
            driver.FindElement(By.Id("btnLogin")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/MainView.aspx");
            driver.FindElement(By.Id("btnAddActivity")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/AddActivity.aspx");
            driver.FindElement(By.Id("ddlActivityType")).Click();
            new SelectElement(driver.FindElement(By.Id("ddlActivityType"))).SelectByText("Carrera");
            driver.FindElement(By.Id("txtName")).Click();
            driver.FindElement(By.Id("txtName")).Clear();
            driver.FindElement(By.Id("txtName")).SendKeys("Series 20\"");
            driver.FindElement(By.Id("txtStartTime")).Click();
            driver.FindElement(By.Id("txtStartTime")).Click();
            driver.FindElement(By.Id("txtStartTime")).Click();
            driver.FindElement(By.Id("txtStartTime")).Clear();
            driver.FindElement(By.Id("txtStartTime")).SendKeys("2025-10-04T18:03");
            driver.FindElement(By.Id("txtStartTime")).Clear();
            driver.FindElement(By.Id("txtStartTime")).SendKeys("2025-10-04T18:35");
            driver.FindElement(By.Id("txtDuration")).Clear();
            driver.FindElement(By.Id("txtDuration")).SendKeys("20");
            driver.FindElement(By.Id("txtDistance")).Clear();
            driver.FindElement(By.Id("txtDistance")).SendKeys("20");
            driver.FindElement(By.Id("txtSlope")).Clear();
            driver.FindElement(By.Id("txtSlope")).SendKeys("50");
            driver.FindElement(By.Id("txtPlaceRunBike")).Clear();
            driver.FindElement(By.Id("txtPlaceRunBike")).SendKeys("Quinta");
            driver.FindElement(By.Id("txtNotes")).Clear();
            driver.FindElement(By.Id("txtNotes")).SendKeys("Series de 1min + 2mins recuperación");
            driver.FindElement(By.Id("btnSave")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/MainView.aspx");
        }
    }
}
