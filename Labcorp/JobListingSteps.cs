using LabcorpPOMs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechTalk.SpecFlow;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Labcorp
{
    [Binding]
    [Scope (Feature = "JobListing")]
    public class JobListingSteps
    {
        private readonly WebElementPOM _webElement;
        private string _jobDescription;
        private IWebDriver _driver;

        public JobListingSteps()
        {
            
            _webElement = new WebElementPOM();
        }

        [Given(@"I am on (.*)")]
        public void GivenIAmOn(string webLink)
        {
            _driver = GetChromDriver();
            _driver.Navigate().GoToUrl(webLink);
        }

        [Given(@"I click on (.*) link")]
        public void GivenIClickOnLink(string careerLink)
        {
            ScrollToBottom(_driver);
            _driver.FindElement(_webElement.testLocator).Click();
            SwitchToWindow(_driver => _driver.Title == "Working at LABORATORY CORP OF AMERICA HOLDINGS | Jobs and Careers at LABORATORY CORP OF AMERICA HOLDINGS");
        }

        [Given(@"Search for '(.*)'")]
        public void GivenSearchForQATestAutomationDeveloper(string jobName)
        {
            
            var searchfield =_driver.FindElement(By.ClassName("search-keyword"));
            ScrollTo200(_driver);
            searchfield.SendKeys(jobName);
            _driver.FindElement(By.ClassName("search-location")).Clear();
            _driver.FindElement(By.ClassName("search-form__submit")).Click();

        }

        [When(@"I Select '(.*)' – '(.*)' – \(posted on\) '(.*)'")]
        public void WhenISelectPostedOn(string value1, string value2, string value3)
        {
            var _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement
                (By.CssSelector("#search-results-list > ul > li:nth-of-type(1)"))));
            var element = _driver.FindElement(By.CssSelector("#search-results-list > ul > li:nth-of-type(1)"));
            ScrollToElement(_driver, element);

            if (element.Text.Contains(value1))
            {
                if (element.Text.Contains(value2))
                {
                    if (element.Text.Contains(value3))
                    {
                        element.Click();
                    }
                    else
                    {
                        Assert.True(false, $"Posted date not found: {value3}");
                    }
                }
                else
                {
                    Assert.True(false, $"Location name not found: {value2}");
                }
            }
            else
            {
                Assert.True(false, $"Job Name not found: {value1}");
            }

        }

        [When(@"Confirm job title, job location, and job id '(.*)'")]
        public void GivenConfirmJobTitleJobLocationAndJobId(string jobId)
        {
            var element = _driver.FindElement(By.XPath("//*[@id='content']/div[3]/section[2]/div[1]/span[2]"));
            Assert.True(element.Text.Contains(jobId), $"Job Id not found: {jobId} ");
        }

        [When(@"Confirm first sentence of third paragraph under Description/Introduction '(.*)'")]
        public void WhenConfirmFirstSentenceOfThirdParagraphUnderDescriptionIntroduction(string text)
        {
            _jobDescription = _driver.FindElement(By.ClassName("ats-description")).Text;
            Assert.True(_jobDescription.Contains(text), $"Description Introduction not found {text}");
        }

        [When(@"Confirm second bullet point under Management Support as '(.*)'")]
        public void WhenConfirmSecondBulletPointUnderManagementSupportAs(string text)
        {
            Assert.True(_jobDescription.Contains(text), $"second bullet point not found {text}");
        }

        [When(@"Confirm third requirement as '(.*)'")]
        public void WhenConfirmThirdRequirementAs(string text)
        {
            Assert.True(_jobDescription.Contains(text), $"third requirement not found {text}");
        }

        [When(@"Confirm first suggested automation tool to be familiar with contains '(.*)'")]
        public void WhenConfirmFirstSuggestedAutomationToolToBeFamiliarWithContains(string text)
        {
            Assert.True(_jobDescription.Contains(text), $"first suggested automation not found {text}");
        }

        [Then(@"Click Apply Now and confirm points (.*) and (.*) in the proceeding page\.")]
        public void ThenClickApplyNowAndConfirmPointsAndInTheProceedingPage_(int p0, int p1)
        {
            _driver.FindElement(By.XPath("//*[@id='content']/div[3]/section[2]/div[2]/div[2]/span/div[4]/span/a")).Click();
        }

        [Then(@"Click to Return to Job Search")]
        public void ThenClickToReturnToJobSearch()
        {
            var _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
             _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("close")));
            _driver.FindElement(By.ClassName("close")).Click();
            _driver.FindElement(By.XPath("//*[@id='ae-main-content']/div/div/div[1]/button")).Click();
        }


        [AfterScenario]
        private void QuitDriver()
        {
            _driver.Close();
            _driver.Quit();
        }

        private IWebDriver GetChromDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-Maximized");
            return new ChromeDriver(options);
        }

        private void ScrollToBottom(IWebDriver Driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        private void ScrollTo200(IWebDriver Driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, 200)");
        }

        private void ScrollToElement(IWebDriver Driver, IWebElement webElement)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, 400)");
        }

        public void SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(handle);
                if (predicate(_driver))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }
    }
}
