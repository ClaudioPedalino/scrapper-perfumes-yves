using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using scrapper_perfumes_yves_console.Models;
using System.Reflection;

var dirverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

using (IWebDriver driver = new ChromeDriver(dirverPath))
{
    //Setup Driver
    driver.Navigate().GoToUrl("https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-femeninos");
    driver.Manage().Window.Maximize();

    // 01. Force load the whole data
    var btn = driver.FindElement(By.Id("products_feed-btn"));

    while (btn.Enabled && btn.Displayed)
    //for (int i = 0; i < 2; i++) 
    {
        Thread.Sleep(1200);
        btn.Click();
        Thread.Sleep(1200);
    }

    // 02. Get items list
    var items = driver.FindElements(By.ClassName("products-feed__product-wrapper"));
    global::System.Console.WriteLine($"Total items: {items.Count}");


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


        Console.ForegroundColor = currentItem.HasStock ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;

        Console.WriteLine($"Name is: {currentItem.Name} || Price is: {currentItem.Price}");
        Console.WriteLine($"Url is: {currentItem.DetailUrl}");
        //Console.WriteLine($"Image sourse is: {currentItem.ImageUrl}");

        if (currentItem.HasStock)
            Console.WriteLine($"Stock status is: Avaiable");
        else
            Console.WriteLine($"Stock status is: No stock");

        Console.WriteLine("...");

        Console.ResetColor();
    }


    //Close Driver
    Console.WriteLine("End!");
    driver.Quit();
    Environment.Exit(0);
}

