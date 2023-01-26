using OpenQA.Selenium;

namespace scrapper_perfumes_yves_console.Extension
{
    internal static class DriverExtensions
    {
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
