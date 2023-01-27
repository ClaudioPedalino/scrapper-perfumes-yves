using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Overview> GetOverview();
        void ResetYvesData();


    }
}
