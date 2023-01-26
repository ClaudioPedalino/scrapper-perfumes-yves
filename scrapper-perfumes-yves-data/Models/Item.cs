using System.ComponentModel.DataAnnotations;

namespace scrapper_perfumes_yves_data.Models
{
    public class Item
    {
        [Key] public Guid Id { get; set; }

        public string Name { get; set; }
        public string Price { get; set; }
        public bool HasStock { get; set; }
        public string DetailUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Tag { get; set; }
    }
}
