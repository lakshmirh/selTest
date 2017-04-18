using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.ObjectModel;

namespace Wikipedia
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver = new FirefoxDriver();
        public void GoToContactUsPage()
        {
            try
            {
                driver.Navigate().GoToUrl("http://www.acumenci.com");
                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath(".//*[@id='masthead']/button")).Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath(".//*[@id='menu-item-74']/a")).Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                driver.Quit();
            }

        }

        public void CloseDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void AccurateAddress()
        {
            try
            {
                GoToContactUsPage();
                String Address = driver.FindElement(By.XPath(".//*[@id='contact_anchor']/div[3]/div/p[1]/a")).Text;
                Console.WriteLine(Address);
                if (Address.Contains("TW9 1HY"))
                    Console.WriteLine("Address is validated");
                else
                    Assert.Fail();
                CloseDriver();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

            }


        }


        [TestMethod]
        public void SubmissionValidation()
        {
            try
            {
                GoToContactUsPage();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath(".//*[@id='content']/div[2]/a[3]/div/h3")).Click();
                Thread.Sleep(2000);

                ReadOnlyCollection<IWebElement> JobOpportunity = driver.FindElements(By.ClassName("section-heading"));

                String value;
                for (int k = 0; k < JobOpportunity.Count; k++)
                {
                    value = JobOpportunity[k].Text;
                    if (value.Contains("tester"))
                    {
                        Console.WriteLine("tester found");
                        break;
                    }
                    else
                        Console.WriteLine("Not found");

                }
                CloseDriver();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //New scenario
        //To Test Connect with us
        //Given I am on Contact page
        //when i click on connect with us
        //then it takes to linked in profile page
        [TestMethod]
        public void ConnectWithUs()
        {
            try
            {
                GoToContactUsPage();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath(".//*[@id='contact_anchor']/div[2]/div/ul/li[2]/a/span")).Click();
                Thread.Sleep(10000);


                String BaseWindow = driver.CurrentWindowHandle;

                ReadOnlyCollection<string> handles = driver.WindowHandles;

                foreach (string handle in handles)
                {

                    if (handle != BaseWindow)
                    {
                        driver.SwitchTo().Window(handle).Title.Contains("Linkedin");
                    }
                }
                Console.WriteLine(driver.Title);
                CloseDriver();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }



    }
}
