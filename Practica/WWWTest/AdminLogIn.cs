using Microsoft.CodeCoverage.Core.Reports.Coverage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SeleniumTests {
    [TestClass]
    public class AdminLogIn {
        
        [TestMethod]
        public void TheAdminLogInTest() {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44396/LogIn.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys("admin@example.com");
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("Admin123456!");
            driver.FindElement(By.Id("btnLogin")).Click();
            Assert.AreEqual("https://localhost:44396/MainView.aspx", driver.Url);

            driver.Close();
            driver.Dispose();
        }
    }
}
