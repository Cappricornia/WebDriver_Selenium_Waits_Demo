using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitExample
{
    public class WebDriverWaitTests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private const string BaseUrl = "http://www.uitestpractice.com/";
       
        [OneTimeSetUp]
        public void OpenBrowser()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = BaseUrl;
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Wait_ThreadSleep()
        {
            var AjaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            AjaxLink.Click();    

            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            Thread.Sleep(20000); 
            
            var textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();
            
            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            Thread.Sleep(20000);
            textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

        }

        [Test]
        public void Test_Wait_ImplicidWait()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            var AjaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            AjaxLink.Click();

            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            var textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

        }

        [Test]
        public void Test_Wait_ExplicitWait()
        {
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));    

            var AjaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            AjaxLink.Click();

            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();


            var textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

        }


        [Test]
        public void Test_Wait_SeleniumExtras_ExpectedConditions()
        {
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            var AjaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            AjaxLink.Click();

            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();


            var textElement = wait.Until(
                ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs")));

            Assert.That(textElement.Text.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            textElement = wait.Until(
                ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs")));

            Assert.That(textElement.Text.Contains("Selenium is a portable software testing"));

        }


        [Test]
        public void Test_Wait_MultipleWaits()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            var AjaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            AjaxLink.Click();

            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            var textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

        }


    }
}