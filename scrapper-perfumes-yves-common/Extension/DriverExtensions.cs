using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using scrapper_perfumes_yves_common.Helpers;

namespace scrapper_perfumes_yves_common.Extension
{
    internal static class DriverExtensions
    {
        public static ChromeDriver GetDriver()
        {
            // https://stackoverflow.com/questions/67858908/try-selenium-download-use-of-latest-chromedriver-exe-in-c-sharp-if-it-fails-to-i
            var chromedriverInstaller = new ChromeDriverInstaller();
            chromedriverInstaller.Install().Wait();


            //return new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void GoTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }
    }
}
