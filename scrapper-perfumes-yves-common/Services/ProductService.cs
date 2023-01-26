using Microsoft.Extensions.Options;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.Models;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Services
{
    public class ProductService : IProductService
    {
        private readonly IOptionsSnapshot<Configuration> _configuration;
        private readonly IProductRepository _repo;

        public ProductService(IOptionsSnapshot<Configuration> configuration, IProductRepository repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        public IEnumerable<Product> GetAll()
        {
            var result = _repo.GetItems();
            return result;
        }

        public IEnumerable<Overview> GetOverview()
        {
            var result = _repo.GetItems();

            var response = result
                .GroupBy(c => new { c.Section, })
                .Select(gcs => new Overview()
                {
                    Section = gcs.Key.Section,
                    WithStock = gcs.Count(x => x.HasStock),
                    WithoutStock = gcs.Count(x => !x.HasStock)
                });

            return response;
        }

        public void ResetYvesData()
        {
            _repo.ResetData();
        }
    }
}
