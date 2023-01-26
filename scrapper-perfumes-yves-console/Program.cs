using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using scrapper_perfumes_yves_console.Common;
using scrapper_perfumes_yves_console.Configuration;
using scrapper_perfumes_yves_console.Extension;
using System.Reflection;

var dirverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

using (IWebDriver driver = new ChromeDriver(dirverPath))
{
    foreach (var site in Config.Sites)
    {
        driver.GoTo(site.Value);
        Scrapper.LoadWholeData(driver);

        var items = Scrapper.GetItemsData(driver);

        Console.WriteLine($"Total items {site.Key}: {items.Count}");
    }

    driver.Finish();
}

