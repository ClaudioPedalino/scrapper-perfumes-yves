using OpenQA.Selenium;
using scrapper_perfumes_yves_common.Models;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Common
{
    internal static class Scrapper
    {
        internal static void LoadWholeData(IWebDriver driver)
        {
            try
            {
                var btn = driver.FindElement(By.Id("products_feed-btn"));

                while (btn.Enabled && btn.Displayed)
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

        internal static List<Product> GetItemsData(IWebDriver driver, Site site)
        {
            var items = driver.FindElements(By.ClassName("products-feed__product-wrapper"));

            var data = new List<Product>();

            foreach (var item in items)
            {
                var media = item
                    .FindElement(By.ClassName("products-feed__product-media"))
                    .FindElement(By.TagName("a"));

                var currentItem = new Product();

                currentItem.Name = item.FindElement(By.TagName("h3")).Text;
                currentItem.Price = Convert.ToDecimal(item.FindElement(By.ClassName("products-feed__product-price")).Text);
                currentItem.DetailUrl = item.FindElement(By.TagName("h3")).FindElement(By.TagName("a")).GetAttribute("href");
                currentItem.ImageUrl = media.FindElement(By.TagName("img")).GetAttribute("src");
                currentItem.HasStock = true;
                currentItem.Section = site.Section;
                currentItem.CreatedAt = DateTime.Now.Date;
                //currentItem.PriceReseller = 0;

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
            }

            return data;
        }
    }
}
