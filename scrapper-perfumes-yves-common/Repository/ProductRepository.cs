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

        public List<Product> GetItems()
        {
            return _dataContext.Products.ToList();
        }


        public void SaveItemsBySite(string section, List<Product> scrappedItems)
        {
            foreach (var entity in _dataContext.Products.Where(x => x.Section == section))
                _dataContext.Products.Remove(entity);

            _dataContext.SaveChanges();

            #region TODO: Upsert (add new, update existing but modified, delete new non-existing
            //var currentDbData = dataContext.Items.ToList();
            //var updated = currentDbData.Intersect(scrappedItems, new YourEqualityComparer());
            //int updatedCount = updated.Count();
            #endregion

            _dataContext.Products.AddRange(scrappedItems);
            _dataContext.SaveChanges();
        }


        public void ResetData()
        {
            foreach (var entity in _dataContext.Products)
                _dataContext.Products.Remove(entity);

            _dataContext.SaveChanges();
        }
    }
}
