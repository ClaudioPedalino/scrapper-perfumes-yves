using OpenQA.Selenium;
using scrapper_perfumes_yves_console.Configuration;
using scrapper_perfumes_yves_console.Models;

namespace scrapper_perfumes_yves_console.Common
{
    internal class Scrapper
    {
        internal static void LoadWholeData(IWebDriver driver)
        {
            try
            {
                var btn = driver.FindElement(By.Id("products_feed-btn"));

                while (btn.Enabled && btn.Displayed)
                //for (int i = 0; i < 2; i++) 
                {
                    Thread.Sleep(1200);
                    btn.Click();
                    Thread.Sleep(1200);
                }

            }
            catch (Exception)
            {

            }
        }

        internal static List<Item> GetItemsData(IWebDriver driver)
        {
            var items = driver.FindElements(By.ClassName("products-feed__product-wrapper"));

            var data = new List<Item>();

            foreach (var item in items)
            {
                var media = item
                    .FindElement(By.ClassName("products-feed__product-media"))
                    .FindElement(By.TagName("a"));

                var currentItem = new Item();

                currentItem.Name = item.FindElement(By.TagName("h3")).Text;
                currentItem.Price = item.FindElement(By.ClassName("products-feed__product-price")).Text;
                currentItem.DetailUrl = item.FindElement(By.TagName("h3")).FindElement(By.TagName("a")).GetAttribute("href");
                currentItem.ImageUrl = media.FindElement(By.TagName("img")).GetAttribute("src");
                currentItem.HasStock = true;

                try
                {
                    var noStock = media.FindElement(By.TagName("span"));
                    if (noStock != null)
                    {
                        currentItem.HasStock = false;
                    }
                }
                catch (Exception)
                {

                }

                data.Add(currentItem);

                if (Config.PrintDataInConsole) Printer.PrintItemDetail(currentItem);
            }

            return data;
        }
    }
}
