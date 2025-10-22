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

namespace SeleniumTests
{
    [TestClass]
    public class AdminLogIn
    {
        public static IEnumerable<object[]> ReadCSV()
        {
            string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "LogIn.csv");

            return File.ReadLines(FilePath)
                .Skip(1) // Saltar la línea de encabezado
                .Select(line => line.Split(','))
                .Select(values => new object[]
                {
                    values[0].Trim(),
                    values[1].Trim(),
                    int.Parse(values[2].Trim())
                });
        }
        [DataTestMethod]
        [DynamicData(nameof(ReadCSV), DynamicDataSourceType.Method)]
        public void TheAdminLogInTest(string email, string password, int is_succes)
        {
            IWebDriver driver = new ChromeDriver();
            string baseURL = "https://www.google.com/";

            driver.Navigate().GoToUrl("https://localhost:44396/LogIn.aspx");
            driver.FindElement(By.Id("txtEmail")).Click();
            driver.FindElement(By.Id("txtEmail")).Clear();
            driver.FindElement(By.Id("txtEmail")).SendKeys(email);
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys(password);
            driver.FindElement(By.Id("btnLogin")).Click();
            if (is_succes == 0)
                Assert.AreEqual("https://localhost:44396/LogIn.aspx", driver.Url);
            else
                Assert.AreEqual("https://localhost:44396/MainView.aspx", driver.Url);

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
    }
}
