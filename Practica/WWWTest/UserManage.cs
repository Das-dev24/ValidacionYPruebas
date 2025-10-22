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
    public class UserManage
    {
        
        [TestMethod]
        public void TheUserManageTest()
        {
            IWebDriver driver;
            string baseURL;
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";

            driver.Navigate().GoToUrl("https://localhost:44396/LogIn.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys("admin@example.com");
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("Admin123456!");
            driver.FindElement(By.Id("btnLogin")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/Login.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys("admin@example.com");
            driver.FindElement(By.Id("txtPassword")).Click();
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("Admin123456!");
            driver.FindElement(By.Id("loginForm")).Submit();
            driver.Navigate().GoToUrl("https://localhost:44396/MainView.aspx");
            driver.FindElement(By.Id("btnProfile")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/AdminUsers.aspx");
            driver.FindElement(By.Id("rptUsers_btnEdit_2")).Click();
            driver.Navigate().GoToUrl("https://localhost:44396/EditUser.aspx?id=3");
            driver.FindElement(By.Id("btnSaveChanges")).Click();

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
    }
}
