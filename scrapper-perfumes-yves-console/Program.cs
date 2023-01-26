using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        var hasStock = true;

        var name = item.FindElement(By.TagName("h3")).Text;
        var price = item.FindElement(By.ClassName("products-feed__product-price")).Text;
        var url = item.FindElement(By.TagName("h3")).FindElement(By.TagName("a")).GetAttribute("href");
        var media = item
            .FindElement(By.ClassName("products-feed__product-media"))
            .FindElement(By.TagName("a"));

        var image = media.FindElement(By.TagName("img")).GetAttribute("src");

        try
        {
            var noStock = media.FindElement(By.TagName("span"));
            if (noStock != null)
            {
                hasStock = false;
            }
        }
        catch (Exception)
        {

        }


        if (hasStock)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        Console.WriteLine($"Name is: {name} || Price is: {price}");
        Console.WriteLine($"Url is: {url}");
        Console.WriteLine($"Image sourse is: {image}");
        Console.WriteLine("...");

        Console.ResetColor();
    }


    //Close Driver
    Console.WriteLine("End!");
    driver.Quit();
    Environment.Exit(0);
}

