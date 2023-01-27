using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Overview>> GetOverview();

        Task ResetDatabase();
    }
}
