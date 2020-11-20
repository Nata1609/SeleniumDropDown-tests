using Autotestai.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autotestai.Test
{
    public class DropdownDemoTest
    {
        private static DropdownDemoPage page;

        [OneTimeSetUp]
        public static void SetUp()

        {
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            page = new DropdownDemoPage(driver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            page.CloseBrowser();
        }

        [Test]
        public void TestDropDown()
        {
            page.SelectFromDropdownByText("Friday")
                .VerifyResult("Friday");
        }

        [TestCase("Florida", "Texas", TestName = "Pasirenkame 2 reiksmes ir patikriname First Selected")]
        [TestCase("Florida", "Ohio", "Texas", TestName = "Pasirenkame 3 reiksmes ir patikriname First Selected")]
        public void TestMultipleDropDown(params string[] selectedElements)
        {
            page.SelectFromMultipleDropdownByValue(selectedElements.ToList())
                .ClickFirstSelectedButton()
                .CheckIsFirstSelectedStateDisplayed(selectedElements.ToList());
        }

        [TestCase("Ohio", "Texas", TestName = "Pasirenkame 2 reiksmes ir patikriname Get All Selected")]
        [TestCase("California", "New York", "Pennsylvania", "Washington", TestName = "Pasirenkame 4 reiksmes ir patikriname Get All Selected")]
        public void TestMultipleDropDownGetAll(params string[] selectedElements)
        {
            page.SelectFromMultipleDropdownByValue(selectedElements.ToList())
                .ClickGetAllSelectedButton()
                .VerifyResultGetAllStates(selectedElements.ToList());
        }

    }
}
