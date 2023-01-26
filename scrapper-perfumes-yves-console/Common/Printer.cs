using scrapper_perfumes_yves_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scrapper_perfumes_yves_console.Common
{
    internal class Printer
    {
        internal static void PrintItemDetail(Item currentItem)
        {
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
    }
}
