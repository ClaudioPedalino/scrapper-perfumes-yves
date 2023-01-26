using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using scrapper_perfumes_yves_common.Common;
using scrapper_perfumes_yves_common.Extension;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Services
{
    public class ScrapperService : IScrapperService
    {
        private readonly IOptionsSnapshot<Configuration> _configuration;
        private readonly IProductRepository _repo;

        public ScrapperService(IOptionsSnapshot<Configuration> configuration, IProductRepository repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        public void ScrapYvesSites()
        {
            using IWebDriver driver = DriverExtensions.GetDriver();

            if (_configuration.Value.LogIn)
                Login(driver);

            foreach (var site in _configuration.Value.Sites)
            {
                driver.GoTo(site.Url);
                Scrapper.LoadWholeData(driver);

                var scrappedItems = Scrapper.GetItemsData(driver, site);
                
                if (_configuration.Value.PrintDataInConsole)
                    Printer.PrintItemDetail(scrappedItems);

                _repo.SaveItemsBySite(site.Section, scrappedItems);
            }

            driver.Quit();
        }


        private void Login(IWebDriver driver)
        {
            driver.GoTo("https://yvesdorgeval.empretienda.com.ar/");

            driver.FindElement(By.ClassName("header-top__user-list"))
                  .FindElements(By.TagName("li"))[1].Click();

            var emailInput = driver.FindElement(By.Id("login_email"));
            emailInput.SendKeys("claudio.dpedalino@gmail.com");

            var input = driver.FindElement(By.Id("login_password"));
            input.SendKeys("Temporal1#");

            input.Submit();
        }
    }
}
