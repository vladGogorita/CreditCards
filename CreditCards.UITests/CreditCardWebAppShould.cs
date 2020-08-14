using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string HomeTitle = "Home Page - Credit Cards";
        private const string AboutUrl = "http://localhost:44108/Home/About";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Manage().Window.Maximize();
                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;

                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
                
                driver.Navigate().Back();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.NotEqual(initialToken, reloadToken);
            }
        }

        [Fact]
        [Trait("Category", "Application")]

        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement firstTableCell = driver.FindElement(By.TagName("td"));
                string firstProduct = firstTableCell.Text;

                Assert.Equal("Easy Credit Card", firstProduct);

                //TODO: check rest of product table
            }
        }
    }
}
