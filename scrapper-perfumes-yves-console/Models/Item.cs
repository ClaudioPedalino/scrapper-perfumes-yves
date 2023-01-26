namespace scrapper_perfumes_yves_console.Models
{
    internal class Item
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public bool HasStock { get; set; }
        public string DetailUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Tag { get; set; }
    }
}
