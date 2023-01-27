using System.ComponentModel.DataAnnotations;

namespace scrapper_perfumes_yves_common.Models
{
    public class Product
    {
        [Key] public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceReseller { get; set; }
        public bool HasStock { get; set; }
        public string DetailUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Section { get; set; }
        public decimal? ResellerDiscount { get; set; }
        public DateTime CreatedAt { get; set; }


        public decimal GetRelleserDiscount()
        {
            return PriceReseller != default
                ? Math.Round(((PriceReseller.Value * 100 / Price / 100) - 1) / 100, 2)
                : default;
        }
    }
}
