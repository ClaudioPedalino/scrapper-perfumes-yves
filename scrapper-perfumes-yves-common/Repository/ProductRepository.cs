using Microsoft.EntityFrameworkCore;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Product>> GetItems()
        {
            return await _dataContext.Products.ToListAsync();
        }


        public async Task SaveItemsBySite(string section, List<Product> scrappedItems)
        {
            foreach (var entity in _dataContext.Products.Where(x => x.Section == section))
                _dataContext.Products.Remove(entity);
            //await _dataContext.SaveChangesAsync();

            #region TODO: Upsert (add new, update existing but modified, delete new non-existing
            //var currentDbData = dataContext.Items.ToList();
            //var updated = currentDbData.Intersect(scrappedItems, new YourEqualityComparer());
            //int updatedCount = updated.Count();
            #endregion

            await _dataContext.Products.AddRangeAsync(scrappedItems);
            await _dataContext.SaveChangesAsync();
        }


        public async Task ResetData()
        {
            foreach (var entity in _dataContext.Products)
                _dataContext.Products.Remove(entity);

            await _dataContext.SaveChangesAsync();
        }
    }
}
