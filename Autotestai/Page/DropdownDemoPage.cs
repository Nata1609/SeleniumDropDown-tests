using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VCSproject;

namespace Autotestai.Page
{
    public class DropdownDemoPage : BasePage
    {

        private const string PageAddress = "https://seleniumeasy.com/test/basic-select-dropdown-demo.html";
        private const string ResultText = "Day selected :- ";
        private const string ResultTextFirstSelected = "First selected option is : ";
        private const string ResultTextGetAllSelected = "Options selected are : ";
        private SelectElement DropDown => new SelectElement(Driver.FindElement(By.Id("select-demo")));
        private IWebElement ResultTextElement => Driver.FindElement(By.CssSelector(".selected-value"));
        private IWebElement ResultTextElementSelected => Driver.FindElement(By.CssSelector(".getall-selected"));
        private SelectElement MultipleDropDown => new SelectElement(Driver.FindElement(By.Id("multi-select")));
        private IWebElement FirstSelectedButton => Driver.FindElement(By.Id("printMe"));
        private IWebElement GetAllSelectedButton => Driver.FindElement(By.Id("printAll"));

        public DropdownDemoPage(IWebDriver webdriver) : base(webdriver) 
        {
            Driver.Url = PageAddress;
        }
        public DropdownDemoPage SelectFromDropdownByText(string text)
        {
            DropDown.SelectByText(text);
            return this;
        }
        public DropdownDemoPage SelectFromDropdownByValue(string text)
        {
            DropDown.SelectByValue(text);
            return this;
        }
        public DropdownDemoPage VerifyResult(string selectedDay)
        {
            Assert.IsTrue(ResultTextElement.Text.Equals(ResultText + selectedDay), $"Result is wrong, not {selectedDay}");
            return this;
        }
        public DropdownDemoPage SelectFromMultipleDropdownByValue(List<string> listOfStates)
        {
            Actions action = new Actions(Driver);
            action.KeyDown(Keys.Control);
            
            foreach (IWebElement option in MultipleDropDown.Options)
            {
                if (listOfStates.Contains(option.GetAttribute("value")))
                {
                    action.Click(option);
                    /*for (int i = 0; i <= listOfStates.Count; i++)
                    {
                        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                        wait.Until(ExpectedConditions.ElementToBeSelected(option));
                    } */
                    Thread.Sleep(2000);
                } 
            
            }
            action.KeyUp(Keys.Control);
            action.Build().Perform();
            return this;
        }
        public DropdownDemoPage ClickFirstSelectedButton()
        {
            FirstSelectedButton.Click();
            return this;
        }

        public DropdownDemoPage ClickGetAllSelectedButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(GetAllSelectedButton)); 
            GetAllSelectedButton.Click();
            return this;
        }
        public DropdownDemoPage CheckIsFirstSelectedStateDisplayed(List<string> listOfStates)
        {
            Assert.AreEqual(ResultTextElementSelected.Text, ResultTextFirstSelected + listOfStates[0], $"Result is wrong, not {listOfStates[0]}");
            return this;
        }
        public DropdownDemoPage CheckIsGetAllSelectedStateDisplayedFor2(List<string> listOfStates)
        {
                Assert.AreEqual($"{ResultTextGetAllSelected}{listOfStates[0]},{listOfStates[1]}", ResultTextElementSelected.Text, $"Result is wrong, not {listOfStates[0]},{listOfStates[1]}");
                return this;
        }
        public DropdownDemoPage CheckIsGetAllSelectedStateDisplayedFor4(List<string> listOfStates)
        {
            Assert.AreEqual($"{ResultTextGetAllSelected}{listOfStates[0]},{listOfStates[1]},{listOfStates[2]},{listOfStates[3]}", ResultTextElementSelected.Text, $"Result is wrong, not {listOfStates[0]},{listOfStates[1]},{listOfStates[2]},{listOfStates[3]}");
            return this;
        }

        public DropdownDemoPage VerifyResultGetAllStates(List<string> listOfStates)
        {
            int countOfList = listOfStates.Count();

            if (countOfList == 2)
            {
                CheckIsGetAllSelectedStateDisplayedFor2(listOfStates);
            }
            else if (countOfList == 4)
            {
                CheckIsGetAllSelectedStateDisplayedFor4(listOfStates);
            }
                return this;
        }


    }
}
