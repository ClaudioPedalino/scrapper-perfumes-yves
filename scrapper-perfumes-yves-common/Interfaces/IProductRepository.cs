using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetItems();
        void SaveItemsBySite(string section, List<Product> scrappedItems);
    }
}