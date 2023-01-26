using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Common
{
    internal class Printer
    {
        internal static void PrintItemDetail(List<Product> itemList)
        {
            foreach (var currentItem in itemList)
            {
                Console.ForegroundColor = currentItem.HasStock ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;

                Console.WriteLine($"Name is: {currentItem.Name} || Price is: {currentItem.Price}");
                Console.WriteLine($"Url is: {currentItem.DetailUrl}");
                Console.WriteLine($"Image sourse is: {currentItem.ImageUrl}");

                if (currentItem.HasStock)
                    Console.WriteLine($"Stock status is: Avaiable");
                else
                    Console.WriteLine($"Stock status is: No stock");

                Console.WriteLine("...");

                Console.ResetColor();
            }
        }
    }
}
