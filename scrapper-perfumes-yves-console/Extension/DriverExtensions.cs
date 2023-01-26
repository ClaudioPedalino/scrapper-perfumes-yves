using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace scrapper_perfumes_yves_console.Extension
{
    internal static class DriverExtensions
    {
        public static ChromeDriver GetDriver()
        {
            return new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        public static void GoTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public static void Finish(this IWebDriver driver)
        {
            Console.WriteLine("End!");
            driver.Quit();
            Environment.Exit(0);
        }
    }
}
