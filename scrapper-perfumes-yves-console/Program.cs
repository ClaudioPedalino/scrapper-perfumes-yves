using OpenQA.Selenium;
using scrapper_perfumes_yves_console.Common;
using scrapper_perfumes_yves_console.Configuration;
using scrapper_perfumes_yves_console.Extension;
using scrapper_perfumes_yves_data;

using (IWebDriver driver = DriverExtensions.GetDriver())
{
    foreach (var site in Config.Sites)
    {
        driver.GoTo(site.Value);
        Scrapper.LoadWholeData(driver);

        var items = Scrapper.GetItemsData(driver);

        //Console.WriteLine($"Total items {site.Key}: {items.Count}");




        using var dataContext = new DataContext();
        
        // 01 obtener items actuales
        var currentData = dataContext.Items.Count();

        // 02 Filtrar actualizaciones

        // 03 Actualizar
        dataContext.Items.AddRange(items);
        dataContext.SaveChanges();
    }

    driver.Finish();
}

