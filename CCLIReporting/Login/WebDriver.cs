using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCLIReporting
{
    public class WebDriver : IDisposable
    {
        private readonly RemoteWebDriver driver;

        public String jwt_token;

        public WebDriver(string username, string password)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();            
            //service.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            ////options.AddArgument("--window-position=-32000,-32000");
            driver = new ChromeDriver(service, options);

            this.Login(username, password);
        }
        /// <summary>
        /// Obtain JWT token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>JWT Token as a string</returns>
        private void Login(String username, String password)
        {
            this.driver.Navigate().GoToUrl("https://profile.ccli.com/account/signin?appContext=OLR&returnUrl=https%3A%2F%2Freporting.ccli.com%2F");
            this.driver.FindElementById("EmailAddress").SendKeys(username);
            this.driver.FindElementById("Password").SendKeys(password);
            this.driver.FindElementById("sign-in").Click();
            this.Wait();
            this.jwt_token = this.driver.Manage().Cookies.GetCookieNamed("CCLI_JWT_AUTH").Value;            
        }

        public void logout()
        {
            this.driver.Navigate().GoToUrl("https://profile.ccli.com/account/signout?appContext=OLR&returnUrl=https://reporting.ccli.com/");
        }

        /*    public IList<CCLISong> search(String term)
            {
                IList<CCLISong> list = new List<CCLISong>();

                this.driver.Navigate().GoToUrl("https://olr.ccli.com/search/results?SearchTerm=" + Uri.EscapeUriString(term) + "&PageSize=100&AllowRedirect=False");
                this.wait();
                ICollection<IWebElement> elements = this.driver.FindElementById("searchResults").FindElements(By.TagName("td"));

                foreach (var item in elements.Take(10))
                {
                    var ViewSong = item.FindElement(By.ClassName("searchResultsSongSummary")).FindElement(By.ClassName("row"));
                    var uuid = item.FindElement(By.ClassName("searchResultsSongSummary")).GetAttribute("id").Replace("song-", String.Empty);
                    ViewSong.FindElement(By.TagName("a")).Click();
                    this.wait();
                    var dataModalId = ViewSong.FindElement(By.TagName("a")).GetAttribute("data-reveal-id");
                    var detailsDiv = this.driver.FindElementById(dataModalId);

                    var title = detailsDiv.FindElement(By.TagName("h3")).Text;
                    var ccliId = detailsDiv.FindElement(By.TagName("h4")).Text;

                    bool publicDomain = false;
                    string unparsedAuthors = "";
                    try
                    {
                        unparsedAuthors = detailsDiv.FindElements(By.ClassName("secondarySongAttributes"))[0].Text;
                        publicDomain = detailsDiv.FindElements(By.ClassName("secondarySongAttributes"))[1].Text.Contains("Public Domain");
                    }
                    catch (Exception e)
                    {
                        // ignore
                    }

                    detailsDiv.FindElement(By.ClassName("close-reveal-modal")).Click();
                    this.wait();

                    var ReportSong = item.FindElement(By.ClassName("searchResultsSongSummary")).FindElement(By.ClassName("row"));

                    var song = new CCLISong(title, unparsedAuthors, ccliId, publicDomain);
                    list.Add(song);
                }
                return list;
            }*/

        private void Wait()
        {
            IWait<OpenQA.Selenium.IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.driver.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
