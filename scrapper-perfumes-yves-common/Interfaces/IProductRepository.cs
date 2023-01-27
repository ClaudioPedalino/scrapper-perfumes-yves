using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetItems();
        Task ResetData();
        Task SaveItemsBySite(string section, List<Product> scrappedItems);
    }
}