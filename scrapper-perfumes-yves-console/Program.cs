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
    global::System.Console.WriteLine(   items.Count);


    //Close Driver
    driver.Quit();
    Environment.Exit(0);
}

Console.WriteLine("End!");
