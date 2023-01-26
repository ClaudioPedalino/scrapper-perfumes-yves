using OpenQA.Selenium;
using scrapper_perfumes_yves_common.Configuration;
using scrapper_perfumes_yves_console.Common;
using scrapper_perfumes_yves_console.Extension;
using scrapper_perfumes_yves_data.Util;

Config configuration = ConfigurationFile.GetConfiguration();

using (IWebDriver driver = DriverExtensions.GetDriver())
{
    foreach (var site in configuration.Sites)
    {
        driver.GoTo(site.Url);
        Scrapper.LoadWholeData(driver);

        var scrappedItems = Scrapper.GetItemsData(driver, site);

        DataUtil.SaveItemsBySite(site.Section, scrappedItems);
    }

    driver.Finish();
}

