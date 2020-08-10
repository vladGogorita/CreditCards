using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/");

                DemoHelper.Pause();

                string pageTitle = driver.Title;

                Assert.Equal("Home Page - Credit Cards", pageTitle);
                Assert.Equal("http://localhost:44108/", driver.Url);
            }
        }
    }
}
